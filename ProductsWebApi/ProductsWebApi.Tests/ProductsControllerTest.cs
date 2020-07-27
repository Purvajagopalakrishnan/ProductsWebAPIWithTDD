using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsWebApi.Controllers;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductsWebApi.Tests
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductsService> _mockproductsService;
        private ProductsController _productsController;
        private readonly Fixture _fixture;
        private readonly Mock<ILogger<ProductsController>> _logger;

        public ProductControllerTest()
        {
            _mockproductsService = new Mock<IProductsService>();
            _logger = new Mock<ILogger<ProductsController>>();
            _productsController = new ProductsController(_mockproductsService.Object, _logger.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetProducts_ReturnsOkObjectResult()
        {
            var expectedResult = _fixture.Build<Product>().CreateMany().ToList();
            _mockproductsService.Setup(x => x.GetProducts()).ReturnsAsync(expectedResult);
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.GetProducts());
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<List<Product>>(actualResult.Value);
            Assert.NotNull(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task GetProducts_ReturnsNullResult()
        {
            _mockproductsService.Setup(x => x.GetProducts()).ReturnsAsync((List<Product>)null);
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.GetProducts());
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Null(actualResult.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task GivenProductId_ValidData_ReturnProduct()
        {
            var expectedResult = _fixture.Build<Product>().Create();
            _mockproductsService.Setup(x => x.GetProductById(expectedResult.Id)).ReturnsAsync(expectedResult);
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.GetProductById(expectedResult.Id));
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<List<Product>>(actualResult.Value);
            Assert.NotNull(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task GetProducts_ReturnsInternalServerErrorObjectResult()
        {
            var expectedException = _fixture.Create<Exception>();

            _mockproductsService.Setup(x => x.GetProducts()).Throws(expectedException);
            var actualException = Assert.IsType<ObjectResult>(await _productsController.GetProducts());

            Assert.Equal(StatusCodes.Status500InternalServerError, actualException.StatusCode);
            Assert.IsType<Exception>(actualException.Value);
            Assert.Equal(expectedException, actualException.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ReturnsOkObjectResult()
        {
            var expectedResult = _fixture.Build<Product>().Create();

            _mockproductsService.Setup(x => x.AddProduct(expectedResult)).ReturnsAsync(expectedResult);

            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.AddProduct(expectedResult));

            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<Product>(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ReturnsInternalServerErrorObjectResult()
        {
            var productRequestObj = _fixture.Build<Product>().Create();
            var expectedException = _fixture.Create<Exception>();

            _mockproductsService.Setup(x => x.AddProduct(productRequestObj)).Throws(expectedException);
            var actualException = Assert.IsType<ObjectResult>(await _productsController.AddProduct(productRequestObj));

            Assert.Equal(StatusCodes.Status500InternalServerError, actualException.StatusCode);
            Assert.IsType<Exception>(actualException.Value);
            Assert.Equal(expectedException, actualException.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkObjectResult()
        {
            var expectedResult = _fixture.Build<Product>().Create();

            _mockproductsService.Setup(x => x.UpdateProduct(expectedResult.Id, expectedResult)).ReturnsAsync(expectedResult);

            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.UpdateProduct(expectedResult.Id, expectedResult));
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<Product>(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ReturnsInternalServerErrorObjectResult()
        {
            var productRequestObj = _fixture.Build<Product>().Create();
            var expectedException = _fixture.Create<Exception>();

            _mockproductsService.Setup(x => x.UpdateProduct(productRequestObj.Id, productRequestObj)).Throws(expectedException);
            var actualException = Assert.IsType<ObjectResult>(await _productsController.UpdateProduct(productRequestObj.Id, productRequestObj));

            Assert.Equal(StatusCodes.Status500InternalServerError, actualException.StatusCode);
            Assert.IsType<Exception>(actualException.Value);
            Assert.Equal(expectedException, actualException.Value);
            _mockproductsService.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkObjectResult()
        {
            var expectedResult = _fixture.Build<Product>().Create();

            _mockproductsService.Setup(x => x.DeleteProduct(expectedResult.Id)).ReturnsAsync(expectedResult.Id);
            var actualResult = Assert.IsType<ObjectResult>(await _productsController.DeleteProduct(expectedResult.Id));

            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<Exception>(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockproductsService.VerifyAll();
        }
    }
}