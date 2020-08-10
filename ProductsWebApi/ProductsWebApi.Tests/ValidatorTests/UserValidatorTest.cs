using AutoFixture;
using Microsoft.AspNetCore.Identity;
using ProductsWebApi.Models;
using ProductsWebApi.Services;
using ProductsWebApi.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace ProductsWebApi.Tests.ValidatorTests
{
    public class UserValidatorTest
    {
        private readonly Fixture _fixture;
        private readonly UserValidator _userValidator;


        public UserValidatorTest()
        {
            _fixture = new Fixture();
            _userValidator = new UserValidator();
        }

        [Fact]
        public void UserValidatorTests_WhenUserNameisEmpty_ShouldThrowValidationException()
        {
            var user = _fixture.Build<Users>().Create();
            var expected = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);

            var actual = _userValidator.Validate(user);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
        }

        [Fact]
        public void UserValidatorTests_WhenPasswordisEmpty_ShouldThrowValidationException()
        {
            var user = _fixture.Build<Users>().Create();
            user.Password = "";
            user.UserName = "testuser";
            var expected = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);

            var actual = _userValidator.Validate(user);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("testuser","welcome")]
        [InlineData("newuser","welcome123")]
        public void UserValidatorTests_WhenValidUserNameAndPassword_ShouldHaveNoError(string userName, string password)
        {
            var user = new Users { UserName = userName, Password = password };
            var expected = true;

            var actual = _userValidator.Validate(user);

            Assert.Equal(expected, actual.IsValid);
        }
    }
}