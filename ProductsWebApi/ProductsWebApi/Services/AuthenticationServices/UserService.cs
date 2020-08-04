using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Services.AuthenticationServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _userValidator;

        public UserService(IUserRepository userRepository, UserValidator userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public bool IsValidUser(Users userLoginModel)
        {
            _userValidator.ValidateAndThrow(userLoginModel);

            var user = _userRepository.FetchValidUserDetails(userLoginModel.UserName, userLoginModel.Password);
            if (user == null)
            {
                return false;
            }
            else if (user.UserName == userLoginModel.UserName &&
                user.Password == userLoginModel.Password)
            {
                return true;
            }
            else
            {
                throw new ValidationException(ValidationMessages.InvalidUserCredentials);
            }
        }
    }
}
