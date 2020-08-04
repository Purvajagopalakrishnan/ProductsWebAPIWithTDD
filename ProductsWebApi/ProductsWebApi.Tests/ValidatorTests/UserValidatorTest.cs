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

        [Theory]
        [InlineData("", "test")]
        public void UserValidatorTests_WhenUserNameisEmpty_ShouldThrowValidationException(string userName, string password)
        {
            UserValidator userValidator = new UserValidator();
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var actual = userValidator.Validate(user);
            var expected = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("test", "")]
        public void UserValidatorTests_WhenPasswordisEmpty_ShouldThrowValidationException(string userName, string password)
        {
            UserValidator userValidator = new UserValidator();
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var actual = userValidator.Validate(user);
            var expected = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
        }
    }
}