using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.DTO.Authentication;
using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using ANLairQuotationSystem.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Services;

public class AuthenticationService(
        IConfiguration config,
        AppDbContext db,
        TokenGenerator tokenGenerator
    )
{
    private readonly IConfiguration _config = config;
    private readonly AppDbContext _db = db;
    private readonly TokenGenerator _tokenGenerator = tokenGenerator;


    public async Task<Result<bool>> RegisterUser(
            string username,
            string password,
            string firstname,
            string surname,
            string contactNumber,
            string email,
            string? middlename,
            string? extensionName
        )
    {
        // Check user existence first
        if (_db.Users.Any(u => u.Username == username || u.Email == email)) return Result<bool>.Fail("User already exists");

        string newPublicId = StringIdGenerator.Generate();

        User newUser = new()
        {
            PublicId = newPublicId,
            RoleId = 1,
            Username = username,
            Firstname = firstname,
            Middlename = middlename,
            Surname = surname,
            Password = "",
            ExtensionName = extensionName,
            ContactNumber = contactNumber,
            Email = email,
            Status = User.UserStatus.ACTIVE,
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now
        };
        PasswordHasher<User> passwordHasher = new();

        string hashedPw = passwordHasher.HashPassword(newUser, password);

        newUser.Password = hashedPw;

        await _db.Users.AddAsync(newUser);
        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "User created successfully");
    }
    public async Task<Result<AuthenticatedUserResponse>> LoginUser(
            string userIdentifier,
            string password
        )
    {
        User? user = await _db.Users
            .Include(u => u.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Username == userIdentifier || u.Email == userIdentifier);
        if (user == null) return Result<AuthenticatedUserResponse>.Fail("Invalid credentials");

        PasswordHasher<User> passwordHasher = new();

        if (passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
            return Result<AuthenticatedUserResponse>.Fail("Invalid credentials");

        string accessToken = _tokenGenerator.GenerateAccessToken(user);
        string refreshToken = _tokenGenerator.GenerateRefreshToken();

        AuthenticatedUserResponse response = new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        UserSession newSession = new()
        {
            UserId = user.Id,
            RefreshToken = response.RefreshToken,
            IsActive = true,
            DateCreated = DateTime.Now,
            DateExpiring = DateTime.Now.AddDays(double.Parse(_config["JwtSettings:RefreshTokenExpiryInDays"]!))
        };

        await _db.UserSessions.AddAsync(newSession);
        await _db.SaveChangesAsync();

        return Result<AuthenticatedUserResponse>.Ok(response, "Successfully logged in");
    }

    public async Task<Result<AuthenticatedUserResponse>> RefreshToken(string refreshToken)
    {
        UserSession? session = await _db.UserSessions
            .Include(u => u.User)
            .Include(u => u.User.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (session == null) return Result<AuthenticatedUserResponse>.Fail("Invalid request");

        User? user = session.User;
        if (user == null) return Result<AuthenticatedUserResponse>.Fail("Invalid request");

        session.IsActive = false;

        await _db.SaveChangesAsync();

        string newRefreshToken = _tokenGenerator.GenerateRefreshToken();
        string newAccessToken = _tokenGenerator.GenerateAccessToken(user);

        AuthenticatedUserResponse newTokens = new()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };

        // Save new user session    
        UserSession newUserSession = new()
        {
            UserId = user.Id,
            RefreshToken = newRefreshToken,
            IsActive = true,
            DateCreated = DateTime.Now,
            DateExpiring = DateTime.Now.AddDays(double.Parse(_config["JwtSettings:RefreshTokenExpiryInDays"]!))
        };

        await _db.UserSessions.AddAsync(newUserSession);
        await _db.SaveChangesAsync();

        return Result<AuthenticatedUserResponse>.Ok(newTokens, "Successfully refreshed user tokens");
    }

    public async Task<Result<bool>> LogoutUser(string refreshToken)
    {
        UserSession? session = await _db.UserSessions.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (session == null) return Result<bool>.Fail("Session not found");

        session.IsActive = false;

        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successful logout");
    }
}
