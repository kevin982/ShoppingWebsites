using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MVCClient.Controllers
{
    [Controller]
    public class WebsiteController : Controller
    {
        private readonly ILogger<WebsiteController> _logger;

        public WebsiteController(ILogger<WebsiteController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/v1/Website")]
        public IActionResult CreateWebsite()
        {
            return View();
        }

        [HttpPost("/v1/Website")]
        public async Task<IActionResult> CreateWebsite(CreateWebsiteModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/v1/Website/{id}")]
        public async Task<IActionResult> Website(Guid id)
        {
            var website = new
            {
                id = "ff29b522-7bf5-4177-8242-50d44f585b35",
                name = "Kevin Website",
                lat = 9.956681455188745,
                lng = -84.15472107310468,
                description = "This website is for selling computers",
                image =
                    "https://optinmonster.com/wp-content/uploads/2017/10/best-website-builder-for-small-business-min.png"
            };


            ViewBag.Website = JObject.Parse(JsonConvert.SerializeObject(website));

            return View();
        }

        [HttpGet("/v1/Websites")]
        public async Task<string> GetAllOwnerWebsites()
        {
            var content = new[]
            {
                new {websiteId = new Guid("bec567d7-0009-4762-85f6-3b967602dfa2"), websiteName = "MyFirstPage"},
                new {websiteId = new Guid("5aa4529f-2467-410b-82c0-5d6ba69d8490"), websiteName = "MySecondPage"},
                new {websiteId = new Guid("2351cf51-0795-4ba8-936e-cc959f495e43"), websiteName = "MyThirdPage"}
            };

            var result = new
            {
                content = content,
                statusCode = 200,
                title = "All websites have been achieved!",
                succeeded = true
            };

            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("/v1/Website/Search")]
        public IActionResult SearchWebsite()
        {
            return View();
        }

        [HttpGet("/v1/Websites/{category}/{websiteName}/{index}/{quantity}/{sortBy}")]
        public async Task<string> GetWebsites(Guid category, string websiteName, int index, int quantity, string sortBy)
        {
            var content = new[]
            {
                new {websiteId = Guid.NewGuid(), websiteName = "Facebook", description = "Fake description for Facebook", imageUrl = "https://www.facebook.com/images/fb_icon_325x325.png"},
                new {websiteId = Guid.NewGuid(), websiteName = "Google", description = "Fake description for Google", imageUrl = "https://www.muycomputer.com/wp-content/uploads/2020/12/google.png"},
                new {websiteId = Guid.NewGuid(), websiteName = "Apple", description = "Fake description for Apple", imageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Microsoft", description = "Fake description for Microsoft", imageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Amazon", description = "Fake description for Amazon", imageUrl = "https://d500.epimg.net/cincodias/imagenes/2020/07/22/companias/1595442845_918343_1595443241_noticia_normal.jpg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Twitter", description = "Fake description for Twitter", imageUrl = "https://blogs.unsw.edu.au/nowideas/files/2018/03/amazon.jpg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Netflix", description = "Fake description for Netflix", imageUrl = "https://cronicaglobal.elespanol.com/uploads/s1/10/38/05/22/logo-de-netflix-netflix.jpeg"},
            };

            var result = new {content = content, statusCode = 200, title = "Alright", succeeded = true};

            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("/v1/Websites/Saved")]
        public async Task<string> GetSavedWebsites()
        {
            var content = new[]
            {
                new {websiteId = Guid.NewGuid(), websiteName = "Facebook", description = "Fake description for Facebook", imageUrl = "https://www.facebook.com/images/fb_icon_325x325.png"},
                new {websiteId = Guid.NewGuid(), websiteName = "Google", description = "Fake description for Google", imageUrl = "https://www.muycomputer.com/wp-content/uploads/2020/12/google.png"},
                new {websiteId = Guid.NewGuid(), websiteName = "Apple", description = "Fake description for Apple", imageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Microsoft", description = "Fake description for Microsoft", imageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Amazon", description = "Fake description for Amazon", imageUrl = "https://d500.epimg.net/cincodias/imagenes/2020/07/22/companias/1595442845_918343_1595443241_noticia_normal.jpg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Twitter", description = "Fake description for Twitter", imageUrl = "https://blogs.unsw.edu.au/nowideas/files/2018/03/amazon.jpg"},
                new {websiteId = Guid.NewGuid(), websiteName = "Netflix", description = "Fake description for Netflix", imageUrl = "https://cronicaglobal.elespanol.com/uploads/s1/10/38/05/22/logo-de-netflix-netflix.jpeg"},
            };

            var result = new {content = content, statusCode = 200, title = "Alright", succeeded = true};

            return JsonConvert.SerializeObject(result);
        }
        
        [HttpGet("/v1/Website/Saved")]
        public IActionResult SavedWebsites()
        {
            return View();
        }


        [HttpPost("/v1/Website/Save")]
        public async Task<string> SaveWebsite([FromBody]SaveWebsiteModel model)
        {
            if (model is null)
            {
                _logger.LogInformation("The model is null");
            }
            else
            {
                _logger.LogInformation(model.WebsiteId.ToString());
                _logger.LogInformation(model.WebsiteName);
                _logger.LogInformation(model.Description);
                _logger.LogInformation(model.ImageUrl);
            }
            
            var result = new {statusCode = 200, title = "Website saved", succeeded = true};

            return JsonConvert.SerializeObject(result);
        }

        [HttpDelete("/v1/Website/Saved/{websiteId}")]
        public async Task<string> RemoveWebsiteFromSavedWebsites(Guid websiteId)
        {
            _logger.LogInformation(websiteId.ToString());
            
            var result = new {statusCode = 200, title = "Website removed", succeeded = true};

            return JsonConvert.SerializeObject(result);
        } 

        [HttpGet("/v1/Website/IndexesCount/{categoryId}/{size}")]
        public async Task<string> GetWebsitesIndexesCount(Guid categoryId, int size)
        {
            var result = new {content = new {count = 3},statusCode = 200, title = "Website saved", succeeded = true};

            return JsonConvert.SerializeObject(result);
        }
        
    }
}
