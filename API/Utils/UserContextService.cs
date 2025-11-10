using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace InmobiliariaAPI.API.Utils
{
    public interface IUserContextService
    {
        int GetUserId();
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claimValue, out var id) ? id : 0;
        }
    }
}