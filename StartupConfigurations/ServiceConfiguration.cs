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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.StartupConfigurations;

public static class ServiceConfiguration
{
    public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Debug);
        });
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

        services.AddDbContext<CustomerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CustomerDbConnection")));
    }
}