using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    [Controller]
    public class CartController : Controller
    {
        [HttpGet("/v1/Cart")]
        public async Task<IActionResult> Cart()
        {
            var content = new[]
            {
                new { id = "ee07e7c3-fef6-4fb9-9844-f7d2ac56f876", name = "Iphone", amount = 3, price = 999.99, image = "https://i.blogs.es/1f8068/iphone-12-mini-1-/840_560.jpg"},
                new { id = "89576e79-13de-4d8b-98c9-2f17ed762d34", name = "Huawei", amount = 5, price = 899.99, image = "https://i.blogs.es/eaa576/huawei-p50-00/1366_2000.jpeg"},
                new { id = "db9e7454-3461-47b2-89f5-62d112a24ceb", name = "Surface", amount = 1, price = 1999.99, image = "https://m.media-amazon.com/images/I/612OuMs29eL._AC_SY355_.jpg"},
            };

            var result = new { content = content, statusCode = 200, succeeded = true, title = "Shopping cart has been achieved." };

            ViewBag.Result = JObject.Parse(JsonConvert.SerializeObject(result));

            return View();
        }

        [HttpPost("/v1/Cart")]
        public async Task<string> Cart([FromBody] PurchaseModel model)
        {
            var result = new
            {
                title = "All is fine",
                succeeded = true,
                statusCode = 200
            };

            return JsonConvert.SerializeObject(result);
        }

        [HttpDelete("/v1/Cart/{id}")]
        public async Task<string> RemoveProduct(Guid id)
        {
            var result = new
            {
                title = "All is fine",
                succeeded = true,
                statusCode = 200
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}
