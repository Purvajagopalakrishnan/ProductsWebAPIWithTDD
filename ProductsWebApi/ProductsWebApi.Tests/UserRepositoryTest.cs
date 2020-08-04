using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductsWebApi.DBContext;
using ProductsWebApi.Models;
using ProductsWebApi.Repository;
using Xunit;

namespace ProductsWebApi.Tests
{
    public class UserRepositoryTest : MockDBContext
    {
        [Theory]
        [InlineData("user", "123456")]
        public void FetchValidUserDetails_IfInValidUserNameAndPasswordProvided_ReturnsNull(string userName, string password)
        {
            var mockUsersDbSet = new Mock<DbSet<Users>>();
            var UsersList = new List<Users> {
                new Users
                {
                    UserName=userName,
                    Password=password
                }
            };

            mockUsersDbSet = MockDbSet(UsersList);

            var productsDbContextMock = new Mock<IProductsDBContext>();
            productsDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);

            var userRepository = new UserRepository(productsDbContextMock.Object);
            var actual = userRepository.FetchValidUserDetails(userName, password);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData("testuser", "welcome")]
        public void FetchValidUserDetails_IfValidUserNameAndPasswordProvided_ReturnsValidUserModel(string userName, string password)
        {
            var mockUsersDbSet = new Mock<DbSet<Users>>();
            var UsersList = new List<Users> {
                new Users
                {
                    UserName=userName,
                    Password=password
                }
            };

            mockUsersDbSet = MockDbSet(UsersList);

            var productsDbContextMock = new Mock<IProductsDBContext>();
            productsDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);

            var userRepository = new UserRepository(productsDbContextMock.Object);
            var actual = userRepository.FetchValidUserDetails(userName, password);

            Assert.IsType<Users>(actual);
            Assert.Equal(userName, actual.UserName);
            Assert.Equal(password, actual.Password);
        }
    }
}