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
        private readonly Fixture _fixture;
        private readonly UserService _userService;
        private readonly IUserRepository _mockUserRepository;

        public UserServiceTest()
        {
            _fixture = new Fixture();
            _userService = new UserService(Mock.Of<IUserRepository>(), new UserValidator());
            _mockUserRepository = Mock.Of<IUserRepository>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void IsValidUser_IfUserNameIsNullOrEmpty_ThrowsValidationException(string userName)
        {
            var user = _fixture.Build<Users>()
                              .With(x => x.UserName, userName)
                              .Create();


            var expectedException = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => _userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void IsValidUser_IfPasswordIsNullOrEmpty_ThrowsValidationException(string password)
        {
            var user = _fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .Create();

            var expectedException = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);
            
            var actualException = Assert.Throws<ValidationException>(() => _userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("user", "123456")]
        public void IsValidUser_IfInValidUserNameAndPasswordProvided_ThrowsValidationException(string userName, string password)
        {
            var user = _fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var invalidUserDetails = _fixture.Build<Users>()
                        .Create();

            Mock.Get(_mockUserRepository)
                .Setup(x => x.FetchUserDetails(user))
                .ReturnsAsync(false);

            var expectedException = new ValidationException(ValidationMessages.InvalidUserCredentials);
           
            var actualException = Assert.Throws<ValidationException>(() => _userService.IsValidUser(invalidUserDetails));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("testuser", "welcome")]
        public void IsValidUser_IfValidUserNameAndPasswordProvided_ReturnsTrue(string userName, string password)
        {
            var user = _fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            Mock.Get(_mockUserRepository)
                .Setup(x => x.FetchUserDetails(user))
                .ReturnsAsync(true);

            var actual = _userService.IsValidUser(user);

            Assert.True(actual);
        }
    }
}
