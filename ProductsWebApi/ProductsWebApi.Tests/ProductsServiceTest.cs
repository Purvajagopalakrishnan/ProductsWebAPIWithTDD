using AutoFixture;
using FluentAssertions;
using Moq;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductWebApi.Tests
{
    public class ProductsServiceTest
    {
        private readonly Mock<IProductsService> _mockproductService;
        private readonly ProductsService _productService;
        private readonly Fixture _fixture;
        private string expectedNullResult = null;
        private Product productRequestObj = null;

        public ProductsServiceTest()
        {
            _mockproductService = new Mock<IProductsService>();
            _productService = new ProductsService(_mockproductService.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetProduct_ValidData()
        {
            var expextedResult = _fixture.Build<Product>().CreateMany().ToList();
            _mockproductService.Setup(x => x.GetProducts()).ReturnsAsync(expextedResult);
            var actualResult = await _productService.GetProducts();
            actualResult.Should().Equal(expextedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task GetProduct_ReturnsNull()
        {
            _mockproductService.Setup(x => x.GetProducts()).ReturnsAsync((List<Product>)null);
            var actualResult = await _productService.GetProducts();
            actualResult.Should().BeNullOrEmpty();
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task GetProductById_ReturnProduct()
        {
            var expectedResult = _fixture.Build<Product>().Create();
            _mockproductService.Setup(x => x.GetProductById(expectedResult.Id)).ReturnsAsync(expectedResult);
            var actualResult = await _productService.GetProductById(expectedResult.Id);
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ValidData_ReturnsValidData()
        {
            var expextedResult = _fixture.Build<Product>().Create();
            _mockproductService.Setup(x => x.AddProduct(expextedResult)).ReturnsAsync(expextedResult);
            var actualResult = await _productService.AddProduct(expextedResult);
            actualResult.Should().BeEquivalentTo(expextedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_InvalidData_ReturnsNull()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            _mockproductService.Setup(x => x.AddProduct(productRequestObj)).ReturnsAsync((Product)null);
            var actualResult = await _productService.AddProduct(productRequestObj);
            var expectedResult = expectedNullResult;
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ValidData_ReturnsValidData()
        {
            var expectedResult = productRequestObj = _fixture.Build<Product>().Create();
            _mockproductService.Setup(x => x.UpdateProduct(productRequestObj.Id, productRequestObj)).ReturnsAsync(expectedResult);
            var actualResult = await _productService.UpdateProduct(productRequestObj.Id, productRequestObj);
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_InvalidData_ReturnsNull()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            _mockproductService.Setup(x => x.UpdateProduct(productRequestObj.Id, productRequestObj)).ReturnsAsync((Product)null);
            var actualResult = await _productService.UpdateProduct(productRequestObj.Id, productRequestObj);
            var expectedResult = expectedNullResult;
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task DeleteBusinessAsync_ValidData_ReturnsAffectedRowsFromDb()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            var expectedResult = productRequestObj.Id;
            _mockproductService.Setup(x => x.DeleteProduct(productRequestObj.Id)).ReturnsAsync(expectedResult);
            var actualResult = await _productService.DeleteProduct(productRequestObj.Id);
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductService.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_InValidData_ReturnsNull()
        {
            productRequestObj = _fixture.Build<Product>().Create();
            _mockproductService.Setup(x => x.DeleteProduct(productRequestObj.Id)).ReturnsAsync((string)null);
            var actualResult = await _productService.DeleteProduct(productRequestObj.Id);
            var expectedResult = expectedNullResult;
            actualResult.Should().BeEquivalentTo(expectedResult);
            _mockproductService.VerifyAll();
        }
    }
}