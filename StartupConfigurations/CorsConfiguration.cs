namespace CustomerManagementService
{
    public static class CorsConfiguration
    {
        public static void AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy", builder =>
                    builder.WithOrigins("http://localhost:7040")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("ProdPolicy", builder =>
                    builder.WithOrigins("https://thamcoapigatewayproduction.azurewebsites.net")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
    }
}
