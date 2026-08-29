namespace ANLairQuotationSystem.Utilities;

public class TokenOptions(
        IConfiguration configuration
    )
{
    private readonly IConfiguration _configuration = configuration;

    public string RefreshCookieName => _configuration["UtilValues:RefreshTokenCookieName"]!;

    public CookieOptions RefreshTokenOptions => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None, // Change on production, should be strict
        Expires = DateTime.Now.AddDays(double.Parse(_configuration["JwtSettings:RefreshTokenExpiryInDays"]!)),
        Path = "/api/auth"
    };

    public CookieOptions ExpiredRefreshTokenOptions => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None, // Change on production, should be strict
        Expires = DateTime.Now.AddDays(-1),
        Path = "/api/auth"
    };
}
