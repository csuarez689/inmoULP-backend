using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace InmobiliariaAPI.API.Filters;

public class AuthPropietarioFilter : IAsyncActionFilter
{
    private readonly IPropietarioRepository _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthPropietarioFilter(
        IPropietarioRepository repo,
        IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var claimValue = _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (!int.TryParse(claimValue, out var userId) || userId <= 0)
        {
            context.Result = new UnauthorizedObjectResult(
                new { message = "Token inválido" });
            return;
        }

        var propietario = await _repo.GetById(userId, true);
        if (propietario == null)
        {
            context.Result = new UnauthorizedObjectResult(
                new { message = "Usuario no registrado" });
            return;
        }

        context.HttpContext.Items["CurrentPropietario"] = propietario;
        await next();
    }
}