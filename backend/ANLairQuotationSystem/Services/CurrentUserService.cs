using System.Security.Claims;

namespace ANLairQuotationSystem.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;
    public string PublicId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User not authorized");

    public bool IsInRole(string role) => User?.IsInRole(role) ?? false;
    public string? GetClaim(string claimType) => User?.FindFirst(claimType)?.Value;
}
