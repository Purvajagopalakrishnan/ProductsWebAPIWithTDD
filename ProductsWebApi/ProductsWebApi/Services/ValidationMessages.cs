using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Services
{
    public class ValidationMessages
    {
        public const string UserNameCannotBeEmpty = "User Name cannot be Empty";
        public const string PasswordCannotBeEmpty = "Password cannot be Empty";
        public const string UserNameAndPasswordCannotBeEmpty = "UserName and Password cannot be Empty";
        public const string InvalidUserCredentials = "Invalid User Credentials";
    }
}
