using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CustomerManagementService.StartupConfigurations;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        string authority = $"https://{configuration["Auth0Authority"]}/";
        string? audience = configuration["Auth0Audience"];
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
            });
    }
}