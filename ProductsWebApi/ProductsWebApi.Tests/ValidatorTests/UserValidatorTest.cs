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

        [Theory]
        [InlineData ("", "test")]
        public void UserValidatorTests_WhenUserNameisEmpty_ShouldThrowValidationException(string userName, string password)
        {
            var user = _fixture.Build<Users>().With(x => x.Password, password)
                                          .With(x => x.UserName, userName).Create();
            var expected = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);

            var actual = _userValidator.Validate(user);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
            Assert.False(actual.IsValid);

        }

        [Theory]
        [InlineData("testuser", "")]
        public void UserValidatorTests_WhenPasswordisEmpty_ShouldThrowValidationException(string userName, string password)
        {
            var user = _fixture.Build<Users>().With(x => x.UserName, userName)
                                              .With(x => x.Password, password).Create();
            var expected = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);

            var actual = _userValidator.Validate(user);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
            Assert.False(actual.IsValid);
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
            Assert.True(actual.IsValid);
        }
    }
}