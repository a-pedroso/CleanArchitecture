using CleanArchitecture.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
