using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;

namespace MVCClient.Controllers
{
    [Controller]
    public class PurchaseController : Controller
    {

        [HttpPatch("/v1/Purchase")]
        public async Task<string> CompletePurchase(CompletePurchaseModel model)
        {
            var result = new { statusCode = 204, succeeded = true, title = "All right"};

            return JsonConvert.SerializeObject(result);
        }
        
        [HttpGet("/v1/Purchase/Costumer")]
        public IActionResult CostumerPurchase()
        {
            return View();
        }
        
        [HttpGet("/v1/Purchase/Owner")]
        public IActionResult OwnerPurchase()
        {
            return View();
        }

        [HttpGet("/v1/Purchases/{type}/{category}/{size}/{index}")]
        public async Task<string> GetPurchases(string type, string category, int size, int index)
        {
            var content = new[]
            {
                new {id = Guid.NewGuid(),lat = 9.956682481016328, lng = -84.15474521521875, name = "Fake Product 1", quantity = 1, imageUrl = "https://www.mountaingoatsoftware.com/uploads/blog/2016-09-06-what-is-a-product.png", price = 99.99, status = "Complete"},
                new {id = Guid.NewGuid(),lat = 9.957952747693774, lng = -84.15820462813963, name = "Fake Product 2", quantity = 3, imageUrl = "https://offautan-uc1.azureedge.net/-/media/images/off/ph/products-en/products-landing/landing/off_overtime_product_collections_large_2x.jpg?la=en-ph", price = 99.99, status = "Complete"},
                new {id = Guid.NewGuid(),lat = 9.951989923515011, lng = -84.14822088331445, name = "Fake Product 3", quantity = 4, imageUrl = "https://hbr.org/resources/images/article_assets/2019/11/Nov19_14_sb10067951dd-001.jpg", price = 9.99, status = "Complete"},
                new {id = Guid.NewGuid(),lat = 9.955980096032382, lng = -84.14180276140092, name = "Fake Product 4", quantity = 1, imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAaqRrPohTo7q-QopKrk744mbTdq_qul2mPg&usqp=CAU", price = 8.99, status = "Process"},
                new {id = Guid.NewGuid(),lat = 9.94300812068666, lng = -84.13018034662778, name = "Fake Product 5", quantity = 2, imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpp7JZN_voM95GSpUBMWkJsAw4pPzrazJWOg&usqp=CAU", price = 1.99, status = "Process"},
                new {id = Guid.NewGuid(),lat = 9.943023065597044, lng = -84.12409602753586, name = "Fake Product 6", quantity = 1, imageUrl = "https://images.ctfassets.net/qlyedcbd6576/2la1cL01j7XSRQhvP3xUJ2/534e3a55e11ca88a3c4e3f7a76122d1f/1_brex_final_700.jpg?fm=webp", price = 0.99, status = "Process"}
            };

            var result = new { content = content, succeeded = true, statusCode = 200, title = "All the purchases"};

            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("/v1/Purchase/IndexesCount/{category}/{size}")]
        public async Task<string> GetPurchasesIndexesCount(string category, int size)
        {
            var content = new { count = 3};

            var result = new {content = content, succeeded = true, statusCode = 200, title = "Purchases count"};

            return JsonConvert.SerializeObject(result);
        }

        [HttpPatch("/v1/Purchase/{id}")]
        public async Task<string> UpdatePurchaseStatus(Guid id)
        {
            //Change purchase status

            var result = new { statusCode = 204, title = "Purchase status changed", succeeded = true};

            return JsonConvert.SerializeObject(result);
        }
    }
}