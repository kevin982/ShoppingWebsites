using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Extensions
{
    public static class FormFileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                return (file.ContentType.Contains("image"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
