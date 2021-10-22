using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MVCClient.Services;

namespace MVCClient.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddMvcClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMyAuthentication(Configuration);

            services.AddOcelotClient();

            services.AddMyOwnServices();

            return services;
        }

        private static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options => 
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options => 
                {
                    options.ClientId = Configuration["Oidc:ClientId"];
                    options.Authority = Configuration["Oidc:Authority"];
                    options.ClientSecret = Configuration["Oidc:Secret"];
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.SaveTokens = true;

                    options.Scope.Add("offline_access");
                    options.Scope.Add("profile");
                    options.Scope.Add("role");
                    
                    options.Scope.Add("websitesMs.all");
                    options.Scope.Add("categoryWebsiteMs.all");
                    options.Scope.Add("productsMs.all");
                    options.Scope.Add("categoryProductsMs.all");
                    options.Scope.Add("reviewsMs.all");
                    options.Scope.Add("shoppingCartMs.all");
                    options.Scope.Add("savedWebsitesMs.all");
                    options.Scope.Add("statisticsMs.all");
                    options.Scope.Add("searchWebsiteMs.all");
                    
                    options.ClaimActions.MapUniqueJsonKey("role", "role");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "role"
                    };
 
                    options.ClaimActions.MapUniqueJsonKey("role", "role");

                });

            return services;
        }
 
        private static IServiceCollection AddOcelotClient(this IServiceCollection services)
        {
            
            return services;
        }

        private static IServiceCollection AddMyOwnServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}