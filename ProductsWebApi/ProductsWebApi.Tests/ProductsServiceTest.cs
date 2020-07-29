using AutoFixture;
using FluentAssertions;
using Moq;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductWebApi.Tests
{
    public class ProductsServiceTest
    {
        private readonly Mock<IProductRepository> _mockproductRepository;
        private readonly ProductsService _productService;
        private readonly Fixture _fixture;
        private int expectedNullResult = 0;
        private int expectedSuccessfulResult = 1;
        private Product productRequest = null;

        public ProductsServiceTest()
        {
            _mockproductRepository = new Mock<IProductRepository>();
            _productService = new ProductsService(_mockproductRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetProduct_ValidData()
        {
            //Arrange
            var expextedResult = _fixture.Build<Product>().CreateMany().ToList();
            _mockproductRepository.Setup(x => x.GetProducts()).ReturnsAsync(expextedResult);
            
            //Act
            var actualResult = await _productService.GetProducts();

            //Assert
            actualResult.Should().Equal(expextedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProduct_ReturnsNull()
        {
            //Arrange
            _mockproductRepository.Setup(x => x.GetProducts()).ReturnsAsync((List<Product>)null);

            //Act
            var actualResult = await _productService.GetProducts();

            //Assert
            actualResult.Should().BeNullOrEmpty();
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductById_ReturnProduct()
        {
            //Arrange
            var expectedResult = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.GetProductById(expectedResult.Id)).ReturnsAsync(expectedResult);
            
            //Act
            var actualResult = await _productService.GetProductById(expectedResult.Id);
            
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ValidData_ReturnsNewProductId()
        {
            //Arrange
            var testFixture = _fixture.Build<Product>().Create();
            var expectedResult = testFixture.Id;

            _mockproductRepository.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(expectedResult);

            //Act
            var actualResult = await _productService.AddProduct(testFixture);

            //Assert
            Assert.Equal(actualResult, expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_InvalidData_ReturnsNull()
        {
            //Arrange
            productRequest = _fixture.Build<Product>().Create();
            var expectedResult = expectedNullResult;
            _mockproductRepository.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(expectedResult);
            
            //Act
            var actualResult = await _productService.AddProduct(productRequest);

            //Assert
            Assert.Equal(actualResult, expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ValidData_ReturnsValidData()
        {
            //Arrange
             productRequest = _fixture.Build<Product>().Create();
            var expectedResult = expectedSuccessfulResult;

            _mockproductRepository.Setup(x => x.UpdateProduct(productRequest.Id, productRequest)).ReturnsAsync(expectedResult);

            //Act
            var actualResult = await _productService.UpdateProduct(productRequest.Id, productRequest);

            //Assert
            Assert.Equal(expectedResult, actualResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_InvalidData_ReturnsNull()
        {
            //Arrange
            productRequest = _fixture.Build<Product>().Create();
            var expectedResult = expectedNullResult;
            _mockproductRepository.Setup(x => x.UpdateProduct(productRequest.Id, productRequest)).ReturnsAsync(expectedResult);
            
            //Act
            var actualResult = await _productService.UpdateProduct(productRequest.Id, productRequest);

            //Assert
            Assert.Equal(expectedResult, actualResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_ValidData_ReturnsOkResult()
        {
            //Arrange
            productRequest = _fixture.Build<Product>().Create();
            var expectedResult = productRequest.Id;
            _mockproductRepository.Setup(x => x.DeleteProduct(productRequest.Id)).ReturnsAsync(expectedResult);
            
            //Act
            var actualResult = await _productService.DeleteProduct(productRequest.Id);

            //Assert
            Assert.Equal(expectedResult, actualResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_InValidData_ReturnsNull()
        {
            //Arrange
            productRequest = _fixture.Build<Product>().Create();
            var expectedResult = expectedNullResult;
            _mockproductRepository.Setup(x => x.DeleteProduct(productRequest.Id)).ReturnsAsync(expectedResult);
            
            //Act
            var actualResult = await _productService.DeleteProduct(productRequest.Id);

            //Assert
            Assert.Equal(expectedResult, actualResult);
            _mockproductRepository.VerifyAll();
        }
    }
}