using System.Security.Claims;
using ks.application.Services.Interfaces;

namespace ks.webapi.Services;
public class ClaimsService : IClaimsService
{

    public ClaimsService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
        CurrentUser = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
    }
    public Guid CurrentUser { get; }
}