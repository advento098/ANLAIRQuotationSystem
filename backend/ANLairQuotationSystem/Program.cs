using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using ANLairQuotationSystem.Services;
using ANLairQuotationSystem.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddHttpContextAccessor();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Service registration
builder.Services.AddTransient<CurrentUserService>();
builder.Services.AddTransient<AuthenticationService>();

// Utilities registration
builder.Services.AddTransient<ANLairQuotationSystem.Utilities.TokenOptions>();
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

#region "Database Configuration"

var connectionString = builder.Configuration.GetConnectionString("DevConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString));
    options.UseSnakeCaseNamingConvention();
});

#endregion

#region "Authentication"

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // Ensures expired access tokens are rejected
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)),

        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine(context.Exception.InnerException != null ? context.Exception.InnerException : context.Exception);
            return Task.CompletedTask;
        },

        OnChallenge = context =>
        {
            Console.WriteLine("JWT Challenge");
            return Task.CompletedTask;
        },

        //OnMessageReceived = context =>
        //{
        //    var token = context.Request.Query["access_token"];

        //    var path = context.HttpContext.Request.Path;

        //    if (!string.IsNullOrEmpty(path))
        //    {
        //        context.Token = token;
        //    }

        //    return Task.CompletedTask;
        //}
    };
});

#endregion

#region "CORS"
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("DevPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
