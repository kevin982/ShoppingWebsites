using System;
using System.Threading.Tasks;
using CategoryWebsite_MS.Exceptions;
using CategoryWebsite_MS.Models;
using CategoryWebsite_MS.Models.Entities;
using CategoryWebsite_MS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CategoryWebsite_MS.Controllers
{
    [ApiController]
    [Authorize(Policy = "WebsiteCategoryScope")]
    public class WebsiteCategoryController : ControllerBase
    {
        private readonly ISenderService _senderService;
        private readonly IRepository _repository;

        public WebsiteCategoryController(ISenderService senderService, IRepository repository)
        {
            _senderService = senderService;
            _repository = repository;
        }
    
        [Authorize(Roles = "admin")]
        [HttpPost("/api/v1/WebsiteCategory")]
        public async Task<ActionResult<HateoasResponse>> CreateWebsiteCategory(CreateWebsiteCategoryModel model)
        {
            try
            {
                if (model is null || string.IsNullOrEmpty(model.CategoryName))
                    throw new WebsiteCategoryException("The model to create the website category is invalid.")
                        {StatusCode = 400};

                WebsiteCategory websiteCategory = new() {WebsiteCategoryName = model.CategoryName};

                await _repository.CreateWebsiteCategoryAsync(websiteCategory);

                return Created("https://localhost:9000/api/v1/WebsiteCategory",websiteCategory);
            }
            catch (Exception e)
            {
                return _senderService.SendError(e, GenericLinks.GetCategoryLinks());
            }
        }

        [Authorize(Roles = "owner,costumer")]
        [HttpGet("/api/v1/WebsiteCategory")]
        public async Task<ActionResult<HateoasResponse>> GetAllWebsiteCategories()
        {
            try
            {
                var result = await _repository.GetAllWebsitesCategoriesAsync();

                return Ok(_senderService.SendResponse(result, GenericLinks.GetCategoryLinks(),
                    "All the website categories have been reached."));
            }
            catch (Exception e)
            {
                return _senderService.SendError(e, GenericLinks.GetCategoryLinks());
            }
        }
        


    }
}