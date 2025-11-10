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

    public InmueblesController(IInmuebleRepository inmuebleRepository)
    {
        _inmuebleRepository = inmuebleRepository;
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
    public async Task<ActionResult<InmuebleDto>> UpdateInmueble(
        int id,
        [FromBody] UpdateInmuebleDto dto)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;
        
        var inmueble = await _inmuebleRepository.GetById(id);
        if (inmueble == null || inmueble.propietario_id != propietario.id)
            return NotFound();

        inmueble.direccion = dto.direccion;
        inmueble.ambientes = dto.ambientes;
        inmueble.superficie = dto.superficie;
        inmueble.latitud = dto.latitud;
        inmueble.longitud = dto.longitud;
        inmueble.precio = dto.precio;
        inmueble.disponible = dto.disponible;
        inmueble.tipo_id = dto.tipo_id;
        inmueble.uso_id = dto.uso_id;

        var inmuebleActualizado = await _inmuebleRepository.Update(inmueble);
        return Ok(InmuebleDto.FromEntity(inmuebleActualizado));
    }

}
