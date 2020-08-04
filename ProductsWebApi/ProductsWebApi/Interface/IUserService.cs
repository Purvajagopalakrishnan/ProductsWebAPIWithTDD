using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Interface
{
    public interface IUserService
    {
        bool IsValidUser(Users userLoginModel);
    }
}
