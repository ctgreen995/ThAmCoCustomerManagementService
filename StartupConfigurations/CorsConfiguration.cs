namespace CustomerManagementService
{
    public static class CorsConfiguration
    {
        public static void AddCorsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
                    builder.WithOrigins(allowedOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}