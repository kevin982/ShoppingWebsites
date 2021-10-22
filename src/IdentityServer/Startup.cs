// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityMicroservice.Data;
using IdentityMicroservice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using IdentityMicroservice.Mappers;
using IdentityMicroservice.Services;

namespace IdentityMicroservice
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

#if DEBUG
           services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => 
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(20), errorNumbersToAdd : null);
                }));

            #region Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders()
               .AddRoles<IdentityRole>();


            services.Configure<IdentityOptions>(options =>
            {
                #region Password Configuration
                options.Password.RequiredLength = Configuration.GetValue<int>("PasswordConfig:RequiredLength");
                options.Password.RequiredUniqueChars = Configuration.GetValue<int>("PasswordConfig:RequiredUniqueChars");
                options.Password.RequireDigit = Configuration.GetValue<bool>("PasswordConfig:RequireDigit");
                options.Password.RequireLowercase = Configuration.GetValue<bool>("PasswordConfig:RequireLowerCase");
                options.Password.RequireUppercase = Configuration.GetValue<bool>("PasswordConfig:RequireUpperCase");
                options.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("PasswordConfig:RequireNonAlphanumeric");
                #endregion

                #region Lockout Configuration

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                #endregion

                #region User Configurations

                options.User.AllowedUserNameCharacters = "aábcdeéfghiíjklmnñoópqrstuúvwxyzAÁBCDEÉFGHIÍJKLMNOÓPQRSTUÚVWXYZ0123456789-._@+ ";
                options.User.RequireUniqueEmail = true;

                #endregion

                #region SignIn Configurations

                options.SignIn.RequireConfirmedEmail = true;

                #endregion

                #region LockoutConfigurations

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                options.Lockout.MaxFailedAccessAttempts = 4;

                #endregion

                #region CookiesPolicy

                services.Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;

                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });


                #endregion

            });

            #endregion

            #region IdentityServer4

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            #endregion
 

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(gOptions =>
                {
                    gOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    gOptions.ClientId = Configuration.GetValue<string>("IdentityProviders:Google:Id");
                    gOptions.ClientSecret = Configuration.GetValue<string>("IdentityProviders:Google:Secret");
                });

            #region Services

            services.AddScoped<IAccountMapper, AccountMapper>();
            services.AddScoped<IAccountService, AccountService>();

            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}