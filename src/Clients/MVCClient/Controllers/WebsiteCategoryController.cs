using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    [Controller]
    public class WebsiteCategoryController : Controller
    {
        [HttpGet("/v1/WebsiteCategory")]
        public async Task<IActionResult> CreateWebsiteCategory()
        {
            return View();
        }

        [HttpPost("/v1/WebsiteCategory")]
        public async Task<IActionResult> CreateWebsiteCategory(CreateWebsiteCategoryModel model)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpGet("/v1/WebsiteCategories")]
        public async Task<string> GetAllCategories()
        {
            try
            {
                //Here I must implement the actual code

                
                var content = new[]
                {
                   new { categoryId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), categoryName = "Clothes" },
                   new { categoryId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), categoryName = "Sport" },
                   new { categoryId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), categoryName = "Shoes" }
                };

                var result = new { content = content, statusCode = 200, succeeded = true, title = "The website categories were reached!"};

                return JsonSerializer.Serialize(result);    



            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
