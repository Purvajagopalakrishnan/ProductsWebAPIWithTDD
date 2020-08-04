using AutoFixture;
using FluentValidation;
using Moq;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Services;
using ProductsWebApi.Services.AuthenticationServices;
using ProductsWebApi.Validator;
using System.Linq;
using Xunit;

namespace ProductsWebApi.Tests
{
    public class UserServiceTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsValidUser_IfUserNameIsNullOrEmpty_ThrowsValidationException(string userName)
        {
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.UserName, userName)
                              .Create();

            var userService = new UserService(Mock.Of<IUserRepository>(), new UserValidator());

            var expectedException = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsValidUser_IfPasswordIsNullOrEmpty_ThrowsValidationException(string password)
        {
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .Create();

            var userService = new UserService(Mock.Of<IUserRepository>(), new UserValidator());

            var expectedException = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("user", "123456")]
        public void IsValidUser_IfInValidUserNameAndPasswordProvided_ThrowsValidationException(string userName, string password)
        {
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var invalidUserDetails = fixture.Build<Users>()
                        .Create();

            var mockUserRepository = Mock.Of<IUserRepository>();
            Mock.Get(mockUserRepository)
                .Setup(x => x.FetchValidUserDetails(user.UserName, user.Password))
                .Returns(invalidUserDetails);

            var userService = new UserService(mockUserRepository, new UserValidator());

            var expectedException = new ValidationException(ValidationMessages.InvalidUserCredentials);
            var actualException = Assert.Throws<ValidationException>(() => userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("testuser", "welcome")]
        public void IsValidUser_IfValidUserNameAndPasswordProvided_ReturnsTrue(string userName, string password)
        {
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var mockUserRepository = Mock.Of<IUserRepository>();
            Mock.Get(mockUserRepository)
                .Setup(x => x.FetchValidUserDetails(user.UserName, user.Password))
                .Returns(user);

            var userService = new UserService(mockUserRepository, new UserValidator());

            var actual = userService.IsValidUser(user);

            Assert.True(actual);
        }
    }
}
