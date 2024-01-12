using AutoMapper;
using CustomerManagementService.Authorisation;
using CustomerManagementService.Data;
using CustomerManagementService.Profiles;
using CustomerManagementService.Repository.AccountRepositories;
using CustomerManagementService.Repository.CustomerRepositories;
using CustomerManagementService.Repository.ProfilesRepository;
using CustomerManagementService.Services.AccountServices;
using CustomerManagementService.Services.CustomerServices;
using CustomerManagementService.Services.ProfileServices;
using CustomerManagementService.Services.RequestDeleteCustomerSevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using ThAmCoCustomerApiGateway.Services.Auth0Token;

namespace CustomerManagementService.StartupConfigurations;

public static class ServiceConfiguration
{
    public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var mapperConfig = new MapperConfiguration(m => { m.AddProfile(new MapperProfile()); });
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddControllers();

        services.AddScoped<IAuthorizationHandler, HasPermissionHandler>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerAccountService, CustomerAccountService>();
        services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();
        services.AddScoped<ICustomerProfileService, CustomerProfileService>();
        services.AddScoped<ICustomerProfileRepository, CustomerProfileRepository>();

        if (env.IsDevelopment())
        {
            services.AddScoped<IRequestDeleteCustomerService, FakeRequestDeleteCustomerService>();
        }
        else
        {
            /* In production, use the real RequestDeleteCustomerService but not available in this project,
               for demonstration purposes fake is used.
             */
            services.AddScoped<IRequestDeleteCustomerService, FakeRequestDeleteCustomerService>();
            // AddPollyPolicies(services);
            // services.AddHttpClient<IRequestDeleteCustomerService, RequestDeleteCustomerService>(client =>
            //     {
            //         client.BaseAddress = new Uri(configuration["CustomerDeletionServiceBaseUrl"]);
            //     }).AddPolicyHandlerFromRegistry("retryPolicy")
            //     .AddPolicyHandlerFromRegistry("circuitBreakerPolicy")
            //     .AddHttpMessageHandler(handler => new Auth0TokenHandler(
            //         handler.GetRequiredService<IAuth0TokenService>(),
            //         configuration["Auth0CdsClientId"], configuration["Auth0CdsClientSecret"],
            //         configuration["Auth0CdsAudience"]));
        }

        services.AddDbContext<CustomerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CustomerDbConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));
    }

    private static void AddPollyPolicies(IServiceCollection services)
    {
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        var circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromMinutes(1));


        var registry = services.AddPolicyRegistry();
        registry.Add("retryPolicy", retryPolicy);
        registry.Add("circuitBreakerPolicy", circuitBreakerPolicy);
    }
}