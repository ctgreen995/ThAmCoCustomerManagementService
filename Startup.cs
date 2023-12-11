﻿using System.Security.Claims;
using AutoMapper;
using CustomerDatabase.Data;
using CustomerManagementService.Authorisation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Profiles;

namespace CustomerManagementService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            var domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];

            });

            var mapperConfig = new MapperConfiguration(m => { m.AddProfile(new MapperProfile()); });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReadAccount", policy =>
           policy.Requirements.Add(new HasPermissionRequirement(new[] { "read:account" })));
                options.AddPolicy("CreateAccount", policy =>
                    policy.Requirements.Add(new HasPermissionRequirement(new[] { "create:account" })));
                options.AddPolicy("UpdateAccount", policy =>
                    policy.Requirements.Add(new HasPermissionRequirement(new[] { "update:account" })));
                options.AddPolicy("DeleteAccount", policy =>
                    policy.Requirements.Add(new HasPermissionRequirement(new[] { "delete:account" })));
                options.AddPolicy("ManageAccount", policy =>
                    policy.Requirements.Add(new HasPermissionRequirement(new[] { "manage:account" })));
            });

            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();

            services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CustomerDbConnection"),
                    m => m.MigrationsAssembly("CustomerDatabase"))
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
