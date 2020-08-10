using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly Mock<IProductsDBContext> _productsDbContextMock;

        public UserRepositoryTest()
        {
            _productsDbContextMock = new Mock<IProductsDBContext>();
        }

        [Theory]
        [InlineData("user", "123456")]
        public void FetchUserDetails_IfInValidUserNameAndPasswordProvided_ReturnsNull(string userName, string password)
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
            _productsDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);
            var userRepository = new UserRepository(_productsDbContextMock.Object);
           
            var actual = userRepository.FetchUserDetails(userName, password);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData("testuser", "welcome")]
        public async Task FetchUserDetails_IfValidUserNameAndPasswordProvided_ReturnsTrue(string userName, string password)
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

            var userRepository = new UserRepository(_productsDbContextMock.Object);
            var actual = await userRepository.FetchUserDetails(userName, password);

            Assert.IsType<bool>(actual);
            Assert.True(actual);
        }
    }
}