using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Api.Services;
using ProjectManagement.Api.Shared;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationConfigured(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddSingleton<JwtProvider>();

        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        if (jwtSettings == null) throw new ArgumentNullException(nameof(jwtSettings));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.Issuer,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Convert.FromBase64String(jwtSettings.Key)),
                    };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(UserRole.Employee), policy => policy.RequireRole(nameof(UserRole.Employee)));
            options.AddPolicy(nameof(UserRole.ProjectManager),
                policy => policy.RequireRole(nameof(UserRole.ProjectManager)));
            options.AddPolicy(nameof(UserRole.Admin), policy => policy.RequireRole(nameof(UserRole.Admin)));
            options.AddPolicy(nameof(UserRole.Supervisor), policy => policy.RequireRole(nameof(UserRole.Supervisor)));
        });

        return services;
    }
}