using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Services;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult RequestToken([FromBody]Users userLoginModel)
        {
            try
            {
                if (!_userService.IsValidUser(userLoginModel))
                {
                    return BadRequest(ValidationMessages.InvalidUserCredentials);
                }
                
                return Ok(GenerateJwtToken());
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
        }

        private string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.JWTSecretKey]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration[Constants.JWTIssuer],
                audience: _configuration[Constants.JWTAudience],
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(1)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}