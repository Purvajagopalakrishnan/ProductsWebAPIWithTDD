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

            var user = _userRepository.FetchUserDetails(userLoginModel);
            if (user.Result == false)
            {
                throw new ValidationException(ValidationMessages.InvalidUserCredentials);
            }
            return true;
        }
    }
}
