using ANLairQuotationSystem.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ANLairQuotationSystem.Utilities;

public class TokenGenerator(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public string GenerateAccessToken(User user)
    {
        var secretKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
        var credentials = new SigningCredentials(secretKeys, SecurityAlgorithms.HmacSha256);

        var permissionList = user.Role.RolePermissions.Select(rp => rp.Permission.Name.ToString()).ToArray();
        string permissionString = string.Join(",", permissionList);

        var issuer = _config["JwtSettings:Issuer"];
        var audience = _config["JwtSettings:Audience"];

        string fullName = string.Join(";", [user.Firstname, user.Middlename, user.Surname, user.ExtensionName]);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.PublicId),
            new Claim(ClaimTypes.Name, fullName),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim("permissions", permissionString)
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(double.Parse(_config["JwtSettings:AccessTokenExpiryInMinutes"]!)),
            SigningCredentials = credentials,
            Issuer = issuer,
            Audience = audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _config["JwtSettings:Issuer"],
            ValidAudience = _config["JwtSettings:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!)),
            ValidateLifetime = false // Don't crash if the token is expired
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return null;

        return principal;
    }
}
