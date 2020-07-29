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
        private readonly Mock<IProductsService> _mockProductsService;
        private ProductsController _productsController;
        private readonly Fixture _fixture;
        private readonly Mock<ILogger<ProductsController>> _logger;

        public ProductControllerTest()
        {
            _mockProductsService = new Mock<IProductsService>();
            _logger = new Mock<ILogger<ProductsController>>();
            _productsController = new ProductsController(_mockProductsService.Object, _logger.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetProducts_ReturnsOkObjectResult()
        {
            //Arrange
            var expectedResult = _fixture.Build<Product>().CreateMany().ToList();
            _mockProductsService.Setup(x => x.GetProducts()).ReturnsAsync(expectedResult);

            //Act
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.GetProducts());

            //Assert
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<List<Product>>(actualResult.Value);
            Assert.NotNull(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task GetProducts_ReturnsNullResult()
        {
            //Arrange
            _mockProductsService.Setup(x => x.GetProducts()).ReturnsAsync((List<Product>)null);

            //Act
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.GetProducts());

            //Assert
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Null(actualResult.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task GivenProductId_ValidData_ReturnProduct()
        {
            //Arrange
            var expectedResult = _fixture.Build<Product>().Create();
            _mockProductsService.Setup(x => x.GetProductById(expectedResult.Id)).ReturnsAsync(expectedResult);

            //Act
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.GetProductById(expectedResult.Id));

            //Assert
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<List<Product>>(actualResult.Value);
            Assert.NotNull(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task GetProducts_ReturnsInternalServerErrorObjectResult()
        {
            //Arrange
            var expectedException = _fixture.Create<Exception>();
            _mockProductsService.Setup(x => x.GetProducts()).Throws(expectedException);

            //Act
            var actualException = Assert.IsType<ObjectResult>(await _productsController.GetProducts());

            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, actualException.StatusCode);
            Assert.IsType<Exception>(actualException.Value);
            Assert.Equal(expectedException, actualException.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ReturnsOkObjectResult()
        {
            //Arrange
            var productRequestObject = _fixture.Build<Product>().Create();
            var expectedResult = StatusCodes.Status200OK;
            _mockProductsService.Setup(x => x.AddProduct(productRequestObject)).ReturnsAsync(expectedResult);

            //Act
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.AddProduct(productRequestObject));

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<Product>(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task AddProduct_ReturnsInternalServerErrorObjectResult()
        {
            //Arrange
            var productRequestObj = _fixture.Build<Product>().Create();
            var expectedException = _fixture.Create<Exception>();
            _mockProductsService.Setup(x => x.AddProduct(productRequestObj)).Throws(expectedException);

            //Act
            var actualException = Assert.IsType<ObjectResult>(await _productsController.AddProduct(productRequestObj));

            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, actualException.StatusCode);
            Assert.IsType<Exception>(actualException.Value);
            Assert.Equal(expectedException, actualException.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkObjectResult()
        {
            //Arrange
            var productRequestObject = _fixture.Build<Product>().Create();
            var expectedResult = StatusCodes.Status200OK;
            _mockProductsService.Setup(x => x.UpdateProduct(productRequestObject.Id, productRequestObject)).ReturnsAsync(expectedResult);

            //Act
            var actualResult = Assert.IsType<OkObjectResult>(await _productsController.UpdateProduct(productRequestObject.Id, productRequestObject));

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<Product>(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ReturnsInternalServerErrorObjectResult()
        {
            //Arrange
            var productRequestObj = _fixture.Build<Product>().Create();
            var expectedException = _fixture.Create<Exception>();
            _mockProductsService.Setup(x => x.UpdateProduct(productRequestObj.Id, productRequestObj)).Throws(expectedException);
            
            //Act
            var actualException = Assert.IsType<ObjectResult>(await _productsController.UpdateProduct(productRequestObj.Id, productRequestObj));

            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, actualException.StatusCode);
            Assert.IsType<Exception>(actualException.Value);
            Assert.Equal(expectedException, actualException.Value);
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkObjectResult()
        {
            //Arrange
            var expectedResult = _fixture.Build<Product>().Create();
            _mockProductsService.Setup(x => x.DeleteProduct(expectedResult.Id)).ReturnsAsync(expectedResult.Id);
            
            //Act
            var actualResult = Assert.IsType<ObjectResult>(await _productsController.DeleteProduct(expectedResult.Id));

            //Assert
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsType<Exception>(actualResult.Value);
            Assert.Equal(expectedResult, actualResult.Value);
            _mockProductsService.VerifyAll();
        }
    }
}