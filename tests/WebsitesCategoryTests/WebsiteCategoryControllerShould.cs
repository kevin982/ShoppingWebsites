using CategoryWebsite_MS.Controllers;
using CategoryWebsite_MS.Models;
using CategoryWebsite_MS.Models.Entities;
using CategoryWebsite_MS.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebsitesCategoryTests
{
    public class WebsiteCategoryControllerShould
    {
        private readonly Mock<ISenderService> _senderService = new();
        private readonly Mock<IRepository> _repository = new();

        [Theory]
        [InlineData(true, "")]
        [InlineData(false, "")]
        public async Task ThrowExceptionIfModelIsNotValidInCreatingWebsiteCategory(bool isNull, string name)
        {

            //Arrange

            CreateWebsiteCategoryModel model = (isNull) ? null : new() { CategoryName = name};

            //Act

            _senderService.Setup(s => s.SendError(It.IsAny<Exception>(), It.IsAny<List<Link>>())).Returns(It.IsAny<HateoasResponse>());

            WebsiteCategoryController controller = new(_senderService.Object, _repository.Object);

            var result = await controller.CreateWebsiteCategory(model);

            //Assert

            _repository.Verify(s => s.CreateWebsiteCategoryAsync(It.IsAny<WebsiteCategory>()), Times.Never());
            _senderService.Verify(s => s.SendError(It.IsAny<Exception>(), It.IsAny<List<Link>>()), Times.Once());

        }

        [Fact]
        public async Task CallTheCorrectMethodsInCreatingWebsiteCategory()
        {

            //Arrange

            CreateWebsiteCategoryModel model = new() { CategoryName = "Costum Name" };

            //Act

            _repository.Setup(r => r.CreateWebsiteCategoryAsync(It.IsAny<WebsiteCategory>()));

            WebsiteCategoryController controller = new(_senderService.Object, _repository.Object);

            var result = await controller.CreateWebsiteCategory(model);

            //Assert

            _repository.Verify(s => s.CreateWebsiteCategoryAsync(It.IsAny<WebsiteCategory>()), Times.Once());
            _senderService.Verify(s => s.SendError(It.IsAny<Exception>(), It.IsAny<List<Link>>()), Times.Never());
        }

        [Fact]
        public async Task CallTheCorrectMethodsInGetAllWebsiteCategories()
        {

            //Arrange

            //Act

            _repository.Setup(r => r.GetAllWebsitesCategoriesAsync()).ReturnsAsync(It.IsAny<List<WebsiteCategory>>());

            WebsiteCategoryController controller = new(_senderService.Object, _repository.Object);

            var result = await controller.GetAllWebsiteCategories();

            //Assert

            _repository.Verify(s => s.GetAllWebsitesCategoriesAsync(), Times.Once());
            _senderService.Verify(s => s.SendError(It.IsAny<Exception>(), It.IsAny<List<Link>>()), Times.Never());
        }


    }
}
