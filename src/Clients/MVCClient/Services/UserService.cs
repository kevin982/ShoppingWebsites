using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace MVCClient.Services
{
    public class UserService : IUserService
    {
        public bool ContainsRole(string accessToken, string role)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var roles = handler.ReadJwtToken(accessToken).Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

                return (roles.Contains(role)) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
 
}