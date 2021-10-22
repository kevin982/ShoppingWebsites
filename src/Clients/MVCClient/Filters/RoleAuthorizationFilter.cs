using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Filters
{
    public class RoleAuthorizationFilter : Attribute, IAsyncActionFilter
    {
        private readonly string Role;

        public RoleAuthorizationFilter(string role)
        {
            Role = role;
        }
        
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Antes de ejecutar el action method

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string token = await context.HttpContext.GetTokenAsync("access_token");

            var handler = new JwtSecurityTokenHandler();

            var roles = handler.ReadJwtToken(token).Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

            if (!roles.Contains(Role))
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();

            //Despues de ejecutar el action method
        }
    }
}