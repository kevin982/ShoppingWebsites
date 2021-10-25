using IdentityMicroservice.Mappers;
using IdentityMicroservice.Models;
using IdentityMicroservice.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IdentityMS_Tests.Services
{
    public class AccountServiceShould
    {
        private AccountService service;

        private Mock<IUserStore<ApplicationUser>> store = new();
        private Mock<IConfiguration> Configuration = new();
        private Mock<IAccountMapper> _accountMapper = new();


        #region CustomFindUser

        [Theory]
        [InlineData("f1acce5d-7478-4eef-ab97-2e8e644f0f7f", "")]
        [InlineData("", "dc216aa6-903c-4555-aa00-aaa3e66f5db1")]
        [InlineData("", "")]
        public async Task ThrowExceptionIfParametersAreNullOrEmptyInCustomFindUser(string findBy, string value)
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

           await Assert.ThrowsAsync<Exception>(async () => await service.CustomFindUserAsync(findBy, value));

        }

        [Fact]
        public async Task ThrowExceptionIfFindByIsNotIdNorIsEmailInCustomFindUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            string findBy = "name";
            string value = "dc216aa6-903c-4555-aa00-aaa3e66f5db1";

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CustomFindUserAsync(findBy, value));

        }

        [Fact]
        public async Task ThrowExceptionIfUserIsNullInCustomFindUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            string findBy = "email";
            string value = "dc216aa6-903c-4555-aa00-aaa3e66f5db1";

            ApplicationUser user = null;

            _userManager.Setup(u => u.FindByEmailAsync(value)).ReturnsAsync(user);

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CustomFindUserAsync(findBy, value));

        }

        [Fact]
        public async Task ThrowExceptionIfUserUsedExternalProviderInCustomFindUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            string findBy = "email";
            string value = "dc216aa6-903c-4555-aa00-aaa3e66f5db1";

            ApplicationUser user = new() { UsedExternalProvider = true};

            _userManager.Setup(u => u.FindByEmailAsync(value)).ReturnsAsync(user);

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CustomFindUserAsync(findBy, value));
        }



        #endregion


        #region ChangePassword

        [Fact]
        public async Task ThrowExceptionIfModelIsNullInChangePassword()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.ChangePasswordAsync(It.IsAny<ChangePasswordRequestModel>()));
        }

        [Fact]
        public async Task ThrowExceptionIfResultDidNotSucceedInChangePassword()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            ApplicationUser user = new() { UsedExternalProvider = true };

            IdentityResult result = IdentityResult.Failed(new IdentityError() { Description = "Fake error" });

            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(u => u.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result);

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.ChangePasswordAsync(new ChangePasswordRequestModel()));
        }

 
        #endregion


        #region ConfirmEmail

        [Theory]
        [InlineData("f1acce5d-7478-4eef-ab97-2e8e644f0f7f", "")]
        [InlineData("", "dc216aa6-903c-4555-aa00-aaa3e66f5db1")]
        [InlineData("", "")]
        public async Task ThrowExceptionIfUserIdOrTokenAreNullOrEmptyInConfirmEmail(string token, string userId)
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.ConfirmEmailAsync(token, userId));

        }

        [Fact]
        public async Task ThrowExceptionIfConfirmEmailDidNotSucceedInConfirmEmail()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            IdentityResult result = IdentityResult.Failed(new IdentityError() { Description = "Custom Error"});

            string token = "aa";
            string userId = "ss";

            //Act
            _userManager.Setup(u => u.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(result);

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.ConfirmEmailAsync(token, userId));
        }


        #endregion


        #region CreateUser

        [Fact]
        public async Task ThrowExceptionIfModelIsNullInCreateUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            SignUpRequestModel model = null;

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CreateUserAsync(model));
        }

        [Fact]
        public async Task ThrowExceptionIfTheResultOfCreatingTheUserDidNotSucceedInCreateUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            SignUpRequestModel model = new ();

            IdentityResult createUserResult = IdentityResult.Failed(new IdentityError() { Description = "Fake error"});

            //Act
            _userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(createUserResult);

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CreateUserAsync(model));
        }

        [Fact]
        public async Task ThrowExceptionIfTheResultOfAddingUserRoleDidNotSucceedInCreateUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            SignUpRequestModel model = new();

            IdentityResult createUserResult = IdentityResult.Success;
            
            IdentityResult userRoleResult = IdentityResult.Failed(new IdentityError() { Description = "Fake error" });

            //Act
            _userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(createUserResult);
            _userManager.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(userRoleResult);

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CreateUserAsync(model));
        }

        [Fact]
        public async Task ThrowExceptionIfTheResultOfAddingAdminRoleDidNotSucceedInCreateUser()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            SignUpRequestModel model = new() { Email = "kevinvb612@gmail.com"};

            IdentityResult createUserResult = IdentityResult.Success;

            IdentityResult userRoleResult = IdentityResult.Success;
            
            IdentityResult adminRoleResult = IdentityResult.Failed(new IdentityError() { Description = "Fake error" });

            //Act
            _userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(createUserResult);
            _userManager.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), "admin")).ReturnsAsync(adminRoleResult);
            Configuration.Setup(c => c["AdminEmail"]).Returns(model.Email);

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CreateUserAsync(model));
        }

        [Fact]
        public async Task EliminateUserIfItsBeenCreatedAndAnExceptionHasBeenThrown()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            SignUpRequestModel model = new() { Email = "kevinvb612@gmail.com" };

            IdentityResult createUserResult = IdentityResult.Success;

            IdentityResult userRoleResult = IdentityResult.Success;

            IdentityResult adminRoleResult = IdentityResult.Failed(new IdentityError() { Description = "Fake error" });

            //Act
            _userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(createUserResult);
            _userManager.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), "admin")).ReturnsAsync(adminRoleResult);
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            _userManager.Setup(u => u.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(u => u.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            Configuration.Setup(c => c["AdminEmail"]).Returns(model.Email);

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.CreateUserAsync(model));

            _userManager.Verify(u => u.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()), Times.Once());
            _userManager.Verify(u => u.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Once());
        }

        
        #endregion

        #region ResetPassword

        [Fact] 
        public async Task ThrowExceptionIfModelIsNullInResetPassword()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            ResetPasswordRequestModel model = null;

            //Act

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.ResetPasswordAsync(model));
        }

        [Fact]
        public async Task ThrowExceptionIfResetPasswordDidNotSucceedInResetPassword()
        {
            //Arrange
            Mock<UserManager<ApplicationUser>> _userManager = new(store.Object, null, null, null, null, null, null, null, null);

            ResetPasswordRequestModel model = new();

            IdentityResult result = IdentityResult.Failed(new IdentityError() { Description = "Fake error"});

            //Act

            _userManager.Setup(u => u.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result);

            service = new(_userManager.Object, Configuration.Object, _accountMapper.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => await service.ResetPasswordAsync(model));
        }

        #endregion
    }
}
