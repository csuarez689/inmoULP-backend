using InmobiliariaAPI.Application.DTOs.Inmuebles;
using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/propietarios/inmuebles")]
public class InmueblesController : ControllerBase
{
    private readonly IInmuebleRepository _inmuebleRepository;
    private readonly IWebHostEnvironment _environment;

    public InmueblesController(
        IInmuebleRepository inmuebleRepository,
        IWebHostEnvironment environment)
    {
        _inmuebleRepository = inmuebleRepository;
        _environment = environment;
    }

    [HttpGet("tipos")]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<List<object>>> GetTipos()
    {
        var tipos = await _inmuebleRepository.GetTipos();
        return Ok(tipos.Select(t => new { t.id, t.nombre }));
    }

    [HttpGet("usos")]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<List<object>>> GetUsos()
    {
        var usos = await _inmuebleRepository.GetUsos();
        return Ok(usos.Select(u => new { u.id, u.nombre }));
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<List<InmuebleDto>>> GetInmuebles()
    {
        var propietario = (Domain.Entities.Propietario)HttpContext.Items["CurrentPropietario"]!;
        var inmuebles = await _inmuebleRepository.GetByPropietario(propietario.id);
        
        return inmuebles
            .Select(InmuebleDto.FromEntity)
            .ToList();
    }

    [HttpGet("contratos/vigentes")]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<List<InmuebleDto>>> GetConContratosVigentes()
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        var inmuebles = await _inmuebleRepository.GetConContratosVigentes(propietario.id);

        return inmuebles
            .Select(InmuebleDto.FromEntity)
            .ToList();
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<InmuebleDto>> GetById(int id)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        var inmueble = await _inmuebleRepository.GetById(id);

        if (inmueble == null || inmueble.propietario_id != propietario.id)
            return NotFound();

        return InmuebleDto.FromEntity(inmueble);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<InmuebleDto>> UpdateDisponibilidad(
        int id,
        [FromBody] UpdateDisponibilidadDto dto)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;

        var inmueble = await _inmuebleRepository.GetById(id);
        if (inmueble == null || inmueble.propietario_id != propietario.id)
            return NotFound();

        inmueble.disponible = dto.disponible;

        await _inmuebleRepository.Update(inmueble);
        return Ok(InmuebleDto.FromEntity(inmueble));
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<ActionResult<InmuebleDto>> Create([FromForm] CreateInmuebleDto dto)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;

        if (!await _inmuebleRepository.TipoExists(dto.tipo_id))
        {
            ModelState.AddModelError(nameof(dto.tipo_id), "Tipo de inmueble inexistente");
            return ValidationProblem(ModelState);
        }

        if (!await _inmuebleRepository.UsoExists(dto.uso_id))
        {
            ModelState.AddModelError(nameof(dto.uso_id), "Uso de inmueble inexistente");
            return ValidationProblem(ModelState);
        }

            var inmueble = new Inmueble
            {
                direccion = dto.direccion,
                ambientes = dto.ambientes,
                superficie = dto.superficie,
                latitud = dto.latitud,
                longitud = dto.longitud,
                precio = dto.precio,
                disponible = true,
                tipo_id = dto.tipo_id,
                uso_id = dto.uso_id,
                propietario_id = propietario.id
            };

            var creado = await _inmuebleRepository.Create(inmueble);

        if (dto.imagen is not null && dto.imagen.Length > 0)
        {

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? string.Empty, "uploads", "inmuebles");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"inmueble_{creado.id}_{Guid.NewGuid():N}{Path.GetExtension(dto.imagen.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.imagen.CopyToAsync(stream);
            }

            var relativeUrl = $"/uploads/inmuebles/{fileName}";
            creado = await _inmuebleRepository.SetImagenUrl(creado.id, relativeUrl) ?? creado;
        }

        return CreatedAtAction(nameof(GetById), new { id = creado.id }, InmuebleDto.FromEntity(creado));
    }

}
