using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    [Controller]
    public class ProductCategoryController : Controller
    {
        [HttpGet("/v1/ProductCategory")]
        public IActionResult CreateProductCategory()
        {
            return View();
        }
        
        [HttpPost("/v1/ProductCategory")]
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/v1/ProductCategories/{websiteId}")]
        public async Task<string> GetProductCategories(Guid websiteId)
        {
            //This is a fake implementation
            
            var content = new[]
            {
                new { categoryId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), categoryName = "Clothes" },
                new { categoryId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), categoryName = "Sport" },
                new { categoryId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), categoryName = "Shoes" }
            };

            var result = new { content = content, statusCode = 200, succeeded = true, title = "The product categories were reached!"};

            return JsonSerializer.Serialize(result);    
        }
    }
}
