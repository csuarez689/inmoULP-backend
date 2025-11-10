using InmobiliariaAPI.Domain.Contracts;
using InmobiliariaAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
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


//para poder cargar el bearer en swagger
public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize =
            context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any() == true
            || context.MethodInfo.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any();

        var hasAllowAnonymous =
            context.MethodInfo.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any();

        if (!hasAuthorize || hasAllowAnonymous)
        {
            return;
        }

        operation.Security ??= new List<OpenApiSecurityRequirement>();

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }] = Array.Empty<string>()
        });
    }
}