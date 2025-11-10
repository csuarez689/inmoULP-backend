using InmobiliariaAPI.API.Filters;
using InmobiliariaAPI.Application.DTOs.Contratos;
using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaAPI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/propietarios/inmuebles/{inmuebleId}/contratos")]
public class ContratoController : ControllerBase
{
    private readonly IContratoRepository _contratoRepository;
    private readonly IInmuebleRepository _inmuebleRepository;

    public ContratoController(
        IContratoRepository contratoRepository,
        IInmuebleRepository inmuebleRepository)
    {
        _contratoRepository = contratoRepository;
        _inmuebleRepository = inmuebleRepository;
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<List<ContratoDto>>> GetByInmueble(int inmuebleId)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;

        var inmueble = await _inmuebleRepository.GetById(inmuebleId);
        if (inmueble == null || inmueble.propietario_id != propietario.id)
        {
            return NotFound();
        }

        var contratos = await _contratoRepository.GetByInmueble(inmuebleId);
        var resultado = contratos
            .Select(ContratoDto.FromEntity)
            .ToList();

        return Ok(resultado);
    }
}
