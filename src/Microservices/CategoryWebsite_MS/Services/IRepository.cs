using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryWebsite_MS.Models.Entities;

namespace CategoryWebsite_MS.Services
{
    public interface IRepository
    { 
        Task CreateWebsiteCategoryAsync(WebsiteCategory websiteCategory);
        Task<IEnumerable<WebsiteCategory>> GetAllWebsitesCategoriesAsync();
    }
}