using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
