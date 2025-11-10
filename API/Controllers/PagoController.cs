using InmobiliariaAPI.API.Filters;
using InmobiliariaAPI.Application.DTOs.Pagos;
using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaAPI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/propietarios/contratos/{contratoId}/pagos")]
public class PagoController : ControllerBase
{
    private readonly IPagoRepository _pagoRepository;
    private readonly IContratoRepository _contratoRepository;

    public PagoController(IPagoRepository pagoRepository, IContratoRepository contratoRepository)
    {
        _pagoRepository = pagoRepository;
        _contratoRepository = contratoRepository;
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthPropietarioFilter))]
    public async Task<ActionResult<List<PagoDto>>> GetByContrato(int contratoId)
    {
        var propietario = (Propietario)HttpContext.Items["CurrentPropietario"]!;

        var contrato = await _contratoRepository.GetById(contratoId);
        if (contrato == null || contrato.Inmueble.propietario_id != propietario.id)
        {
            return NotFound();
        }

        var pagos = await _pagoRepository.GetByContrato(contratoId);
        var resultado = pagos
            .Select(PagoDto.FromEntity)
            .ToList();

        return Ok(resultado);
    }
}
