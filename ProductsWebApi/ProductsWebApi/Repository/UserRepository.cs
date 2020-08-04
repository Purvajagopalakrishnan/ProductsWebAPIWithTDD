using ProductsWebApi.DBContext;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IProductsDBContext _productsDBContext;

        public UserRepository(IProductsDBContext productsDBContext)
        {
            _productsDBContext = productsDBContext;
        }
        public Users FetchValidUserDetails(string userName, string password)
        {
            return _productsDBContext.Users.Where(user => user.UserName == userName &&
                                                        user.Password == password)
                                                        .SingleOrDefault();
        }
    }
}
