using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
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
        private Mock<DbSet<Users>> _mockUsersDbset;
        private readonly UserRepository _userRepository;
        private readonly Fixture _fixture;

        public UserRepositoryTest()
        {
            _fixture = new Fixture();
            _productsDbContextMock = new Mock<IProductsDBContext>();
            _mockUsersDbset = new Mock<DbSet<Users>>();
            _userRepository = new UserRepository(Mock.Of<IProductsDBContext>());
        }

        [Theory]
        [InlineData("user", "123456")]
        public async Task FetchUserDetails_IfInValidUserNameAndPasswordProvided_ReturnsFalse(string userName, string password)
        {
            var UsersList = new List<Users> {
                new Users
                {
                    UserName=userName,
                    Password=password
                }
            };

            var user = _fixture.Build<Users>()
                                   .With(x => x.Password, password)
                                   .With(x => x.UserName, userName)
                                   .Create();

            _mockUsersDbset = MockDbSet(UsersList);
            _productsDbContextMock.Setup(x => x.Users).Returns(_mockUsersDbset.Object);
           
            var actual = await _userRepository.FetchUserDetails(user);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("testuser", "welcome")]
        public async Task FetchUserDetails_IfValidUserNameAndPasswordProvided_ReturnsTrue(string userName, string password)
        {
            var UsersList = new List<Users> {
                new Users
                {
                    UserName=userName,
                    Password=password
                }
            };

            var user = _fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            _mockUsersDbset = MockDbSet(UsersList);
            _productsDbContextMock.Setup(x => x.Users).Returns(_mockUsersDbset.Object);

            var actual = await _userRepository.FetchUserDetails(user);

            Assert.IsType<bool>(actual);
            Assert.True(actual);
        }
    }
}