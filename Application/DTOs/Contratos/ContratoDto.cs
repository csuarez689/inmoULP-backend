using System;
using InmobiliariaAPI.Application.DTOs.Inmuebles;
using InmobiliariaAPI.Application.DTOs.Inquilinos;
using InmobiliariaAPI.Domain.Entities;

namespace InmobiliariaAPI.Application.DTOs.Contratos;

public class ContratoDto
{
    public int id { get; set; }
    public DateTime fecha_inicio { get; set; }
    public DateTime fecha_finalizacion { get; set; }
    public double monto_alquiler { get; set; }
    public bool estado { get; set; }
    public InmuebleDto? inmueble { get; set; }
    public InquilinoDto? inquilino { get; set; }

    public static ContratoDto FromEntity(Contrato contrato)
    {
        return new ContratoDto
        {
            id = contrato.id,
            fecha_inicio = contrato.fechaInicio,
            fecha_finalizacion = contrato.fechaFinalizacion,
            monto_alquiler = contrato.montoAlquiler,
            estado = contrato.estado,
            inmueble = contrato.Inmueble is null ? null : InmuebleDto.FromEntity(contrato.Inmueble),
            inquilino = contrato.Inquilino is null ? null : InquilinoDto.FromEntity(contrato.Inquilino)
        };
    }
}
