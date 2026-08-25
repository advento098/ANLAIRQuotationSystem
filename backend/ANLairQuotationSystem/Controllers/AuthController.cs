using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.Services;
using ANLairQuotationSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANLairQuotationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
        IConfiguration config,
        TokenOptions tokenOptions,
        AuthenticationService authService
    ) : ControllerBase
{
    private readonly IConfiguration _config = config;
    private readonly TokenOptions _tokenOptions = tokenOptions;
    private readonly AuthenticationService _authService = authService;

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegistrationPayload payload)
    {
        try
        {
            var result = await _authService.RegisterUser(
                    payload.Username,
                    payload.Password,
                    payload.Firstname,
                    payload.Surname,
                    payload.ContactNumber,
                    payload.Email,
                    payload.Middlename,
                    payload.ExtensionName
                );
            if (!result.IsSuccess) throw new Exception(result.Message);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginPayload payload)
    {
        try
        {
            var loginResult = await _authService.LoginUser(payload.UserIdentifier, payload.Password);
            if (!loginResult.IsSuccess) throw new Exception(loginResult.Message);

            Response.Cookies.Append("refreshToken", loginResult.Value!.RefreshToken, _tokenOptions.RefreshTokenOptions);

            return Ok(Result<string>.Ok(loginResult.Value!.AccessToken, loginResult.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser()
    {
        try
        {
            if (!Request.Cookies.TryGetValue(_tokenOptions.RefreshCookieName, out var refreshTokenCookie))
                return Unauthorized(new ErrorResponse("Invalid request"));

            var logoutResult = await _authService.LogoutUser(refreshTokenCookie);
            if (!logoutResult.IsSuccess) return Unauthorized(new ErrorResponse(logoutResult.Message));

            // Overwrite the cookie with an empty value and the expired date
            Response.Cookies.Append(_tokenOptions.RefreshCookieName, "", _tokenOptions.ExpiredRefreshTokenOptions);

            return Ok(logoutResult);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshUser()
    {
        try
        {
            if (!Request.Cookies.TryGetValue(_tokenOptions.RefreshCookieName, out var refreshTokenCookie))
            {
                return Unauthorized(new ErrorResponse("Invalid request"));
            }

            // TODO: Pass this to a new service for refreshing tokens
            var result = await _authService.RefreshToken(refreshTokenCookie);
            if (!result.IsSuccess) return Unauthorized(new ErrorResponse(result.Message));

            Response.Cookies.Append(_tokenOptions.RefreshCookieName, result.Value!.RefreshToken, _tokenOptions.RefreshTokenOptions);

            return Ok(Result<string>.Ok(result.Value!.AccessToken, result.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }

    public record RegistrationPayload(
            string Username,
            string Password,
            string Firstname,
            string Middlename,
            string Surname,
            string ContactNumber,
            string Email,
            string? ExtensionName
        );
    public record LoginPayload(string UserIdentifier, string Password);
    public record LogoutPayload(string Token);
}
