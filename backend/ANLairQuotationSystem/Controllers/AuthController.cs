using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANLairQuotationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
        AuthenticationService authService
    ) : ControllerBase
{
    private readonly AuthenticationService _authService = authService;

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

            return Ok(loginResult);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser([FromBody] LogoutPayload payload)
    {
        try
        {
            var logoutResult = await _authService.LogoutUser(payload.Token);
            if (!logoutResult.IsSuccess) return Unauthorized(new ErrorResponse(logoutResult.Message));

            return Ok(logoutResult);
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
            string Surname,
            string ContactNumber,
            string Email,
            string Middlename,
            string ExtensionName
        );
    public record LoginPayload(string UserIdentifier, string Password);
    public record LogoutPayload(string Token);
}
