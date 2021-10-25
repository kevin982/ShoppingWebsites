using CategoryWebsite_MS.Data;
using CategoryWebsite_MS.Models.Entities;
using CategoryWebsite_MS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebsitesCategoryTests
{
    public class RepositoryShould
    {
 
        [Fact]
        public async Task CreateWebsiteCategoryCorrectly()
        {
            //Arrange

            Guid websiteCategoryId = Guid.NewGuid();

            WebsiteCategory websiteCategory = new() { WebsiteCategoryName = "My Own Website Category", WebsiteCategoryId = websiteCategoryId};

            var context = await FakeData.InitializeContextAsync();

            Repository repository = new(context);

            await repository.CreateWebsiteCategoryAsync(websiteCategory);

            //Act

            var result = await repository.GetAllWebsitesCategoriesAsync();
            var entityCreated = result.FirstOrDefault(c => c.WebsiteCategoryId == websiteCategoryId);


            //Assert

            Assert.Equal(websiteCategoryId, entityCreated.WebsiteCategoryId);
            Assert.Equal(websiteCategory.WebsiteCategoryName, entityCreated.WebsiteCategoryName);
        }


        [Fact]
        public async Task GetAllWebsiteCategoriesCorrectly()
        {
            //Arrange

            List<WebsiteCategory> websiteCategories = new()
            {
                new WebsiteCategory() { WebsiteCategoryName = "Warehouse Store", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Department Store", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Supermarket", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Hypermarket", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Convenience Store", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "E-Commerce", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Drug Store", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Speciality Store", WebsiteCategoryId= Guid.NewGuid()},
                new WebsiteCategory() { WebsiteCategoryName = "Discount Store", WebsiteCategoryId= Guid.NewGuid()},
            };

            websiteCategories = websiteCategories.OrderBy(w => w.WebsiteCategoryName).ToList();

            var context = await FakeData.InitializeContextAsync();

            Repository repository = new(context);

            //Act

            var result = await repository.GetAllWebsitesCategoriesAsync();
            var resultList = result.OrderBy(w => w.WebsiteCategoryName).ToList();

            //Assert

            Assert.Equal(websiteCategories.Count, resultList.Count);

            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.Equal(websiteCategories[i].WebsiteCategoryName, resultList[i].WebsiteCategoryName);
            }

        }

    }
}
