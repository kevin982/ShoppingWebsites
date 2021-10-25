using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryWebsite_MS.Data;
using CategoryWebsite_MS.Exceptions;
using CategoryWebsite_MS.Models;
using CategoryWebsite_MS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CategoryWebsite_MS.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _dbContext;

        public Repository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateWebsiteCategoryAsync(WebsiteCategory websiteCategory)
        {
            try
            {
                await _dbContext.WebsiteCategories.AddAsync(websiteCategory);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<WebsiteCategory>> GetAllWebsitesCategoriesAsync()
        {
            try
            {
                var categories = await _dbContext
                    .WebsiteCategories
                    .AsNoTracking()
                    .ToListAsync();

                if (categories is null || categories.Count == 0)
                {
                    throw new WebsiteCategoryException("There are not website categories") {StatusCode = 404};
                }

                return categories;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}