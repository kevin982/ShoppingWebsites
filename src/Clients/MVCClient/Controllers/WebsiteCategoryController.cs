using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using MVCClient.Services;
using Newtonsoft.Json.Linq;
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
        private readonly IRequestSender _requestSender;
        private readonly ILogger<WebsiteCategoryController> _logger;
        public WebsiteCategoryController(IRequestSender requestSender, ILogger<WebsiteCategoryController> logger)
        {
            _requestSender = requestSender;
            _logger = logger;
        }

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
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                ViewBag.Result = (JObject)await _requestSender.SendRequestAsync("POST", "/v1/WebsiteCategory", accessToken, true, model);
            }
            catch (Exception)
            {
                JObject error = new();

                error.Add("succeeded", false);
                error.Add("title", "Internal Error");

                ViewBag.Result = error;
            }

            return View();
        }

        [HttpGet("/v1/WebsiteCategories")]
        public async Task<string> GetAllCategories()
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return (string)await _requestSender.SendRequestAsync("GET", "/v1/WebsiteCategory", accessToken);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception happend at {DateTime.UtcNow}. Error message {ex.Message}");

                var error = new
                {
                    succeeded = false,
                    title = "Internal error",
                    statusCode = 500
                };

                return JsonSerializer.Serialize(error);
            }
        }
    }
}
