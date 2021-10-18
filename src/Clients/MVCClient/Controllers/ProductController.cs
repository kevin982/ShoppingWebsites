using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVCClient.Controllers
{
    [Controller]
    public class ProductController : Controller
    {
        [HttpGet("/v1/Product/Create")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost("/v1/Product/Create")]
        public async Task<IActionResult> CreateProduct(CreateProductModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/v1/Product/Category/{id}")]
        public async Task<string> GetAllProductsByCategory(Guid id)
        {
           var content = new[]
           {
                new { productId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), productName = "IPhone" },
                new { productId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), productName = "Android" },
                new { productId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), productName = "Google" }
            };

            var result = new { content = content, succeeded = true, statusCode = 200, title = "Products were achieved!"};

            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("/v1/Product/{categoryId}/{orderBy}/{size}/{index}")]
        public async Task<string> GetProducts(Guid categoryId, string orderBy, string size, int index)
        {
            var content = new[]
            {
                new { productId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), productPrice = 10.50, productName = "IPhone", imageUrl = "https://i.blogs.es/da8cbb/iphone-13-pro/1366_2000.jpg" },
                new { productId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), productPrice = 10.50, productName = "IPhone", imageUrl = "https://i.blogs.es/da8cbb/iphone-13-pro/1366_2000.jpg" },
                new { productId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), productPrice = 10.50, productName = "IPhone", imageUrl = "https://i.blogs.es/da8cbb/iphone-13-pro/1366_2000.jpg" },
                new { productId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), productPrice = 9.99, productName = "Android", imageUrl = "https://www.welivesecurity.com/wp-content/uploads/es-la/2012/12/Logo-Android.png" },
                new { productId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), productPrice = 9.99, productName = "Android", imageUrl = "https://www.welivesecurity.com/wp-content/uploads/es-la/2012/12/Logo-Android.png" },
                new { productId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), productPrice = 9.99, productName = "Android", imageUrl = "https://www.welivesecurity.com/wp-content/uploads/es-la/2012/12/Logo-Android.png" },
                new { productId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), productPrice = 5.00, productName = "Google", imageUrl = "https://www.muycomputer.com/wp-content/uploads/2021/04/FLoC.png" },
                new { productId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), productPrice = 5.00, productName = "Google", imageUrl = "https://www.muycomputer.com/wp-content/uploads/2021/04/FLoC.png" },
                new { productId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), productPrice = 5.00, productName = "Google", imageUrl = "https://www.muycomputer.com/wp-content/uploads/2021/04/FLoC.png" }
            };

            var result = new { content = content, succeeded = true, statusCode = 200, title = "Products were achieved!"};
            
            string a = JsonConvert.SerializeObject(result);

            return a;
        }

        [HttpGet("/v1/Product/IndexesCount/{categoryId}/{size}")]
        public async Task<string> GetProductIndexesCount(Guid categoryId, string size)
        {
            var content = new { count = 3 };

            var result = new { content = content, succeeded = true, statusCode = 200, title = "Products were achieved!"};
            
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("/v1/Product/Update")]
        public async Task<IActionResult> UpdateProductStack()
        {
            return View();
        }

        [HttpPatch("/v1/Product/Update")]
        public async Task<IActionResult> UpdateProductStack([FromBody]UpdateProductModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/v1/Product/Remove")]
        public async Task<IActionResult> RemoveProducts()
        {
            return View();
        }

        [HttpDelete("/v1/Product/Remove")]
        public async Task<IActionResult> RemoveProducts([FromBody]DeleteProductModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/v1/Product/{id}")]
        public async Task<IActionResult> GetProductById()
        {
            var product = new
            {
                id = "ff29b522-7bf5-4177-8242-50d44f585b35",
                name = "Iphone",
                description = "This cellphone is for taking pictures",
                stack = 10,
                price = 999.99,
                image = "https://i.blogs.es/1f8068/iphone-12-mini-1-/840_560.jpg",
                reviews = 5,
                stars = 3
            };



            var result = new { content = product , succeeded = true, statusCode = 200, title = "The product has been reached."};

            ViewBag.Result = JObject.Parse(JsonConvert.SerializeObject(result));


            return View();
        }
    }
}