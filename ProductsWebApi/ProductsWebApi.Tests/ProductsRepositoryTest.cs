using AutoFixture;
using FluentAssertions;
using Moq;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Services;
using ProductsWebAPI.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductsWebApi.Tests
{
    public class ProductsRepositoryTest
    {
        private readonly Mock<IProductRepository> _mockproductRepository;
        private readonly ProductRepository _productRepository;
        private readonly Fixture _fixture;
        private string expectedNullResult = null;
        private Product productRequestObj = null;

        public ProductsRepositoryTest()
        {
            _mockproductRepository = new Mock<IProductRepository>();
            _productRepository = new ProductRepository(_mockproductRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetProduct_ValidData()
        {
            var expextedResult = _fixture.Build<Product>().CreateMany().ToList();
            _mockproductRepository.Setup(x => x.GetProducts()).ReturnsAsync(expextedResult);
            var actualResult = await _productRepository.GetProducts();
            actualResult.Should().Equal(expextedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProduct_ReturnsNull()
        {
            _mockproductRepository.Setup(x => x.GetProducts()).ReturnsAsync((List<Product>)null);
            var actualResult = await _productRepository.GetProducts();
            actualResult.Should().BeNullOrEmpty();
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            var expectedResult = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.GetProductById(expectedResult.Id)).ReturnsAsync(expectedResult);
            var actualResult = await _productRepository.GetProductById(expectedResult.Id);
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ValidData_ReturnsValidData()
        {
            var expextedResult = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.AddProduct(expextedResult)).ReturnsAsync(expextedResult);
            var actualResult = await _productRepository.AddProduct(expextedResult);
            actualResult.Should().BeEquivalentTo(expextedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_InvalidData_ReturnsNull()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.AddProduct(productRequestObj)).ReturnsAsync((Product)null);
            var actualResult = await _productRepository.AddProduct(productRequestObj);
            var expectedResult = expectedNullResult;
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ValidData_ReturnsValidData()
        {
            var expectedResult = productRequestObj = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.UpdateProduct(productRequestObj.Id, productRequestObj)).ReturnsAsync(expectedResult);
            var actualResult = await _productRepository.UpdateProduct(productRequestObj.Id, productRequestObj);
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_InvalidData_ReturnsNull()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.UpdateProduct(productRequestObj.Id, productRequestObj)).ReturnsAsync((Product)null);
            var actualResult = await _productRepository.UpdateProduct(productRequestObj.Id, productRequestObj);
            var expectedResult = expectedNullResult;
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_GivenValidData_ReturnsOkResult()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            var expectedResult = productRequestObj.Id;
            _mockproductRepository.Setup(x => x.DeleteProduct(productRequestObj.Id)).ReturnsAsync(expectedResult);
            var actualResult = await _productRepository.DeleteProduct(productRequestObj.Id);
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProductAsync_InvalidData_ReturnsNull()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            _mockproductRepository.Setup(x => x.DeleteProduct(productRequestObj.Id)).ReturnsAsync((string)null);
            var actualResult = await _productRepository.DeleteProduct(productRequestObj.Id);
            var expectedResult = expectedNullResult;
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductRepository.VerifyAll();
        }
    }
}