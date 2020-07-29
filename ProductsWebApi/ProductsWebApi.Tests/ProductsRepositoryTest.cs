using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsWebApi.DbFactory;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Services;
using ProductsWebAPI.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductsWebApi.Tests
{
    public class ProductsRepositoryTest
    {
        //    private readonly Mock<IDbFactory> _mockDbFactory;
        //    private readonly ProductRepository _productRepository;
        //    private readonly Fixture _fixture;
        //    private string expectedNullResult = null;
        //    private Product productRequest = null;
        //    private readonly Mock<ILogger<ProductRepository>> _logger;

        //    public ProductsRepositoryTest()
        //    {
        //        _mockDbFactory = new Mock<IDbFactory>();
        //        _logger = new Mock<ILogger<ProductRepository>>();
        //        _productRepository = new ProductRepository(_mockDbFactory.Object, _logger.Object);
        //        _fixture = new Fixture();
        //    }

        //    [Fact]
        //    public async Task GetProduct_ValidData()
        //    {
        //        //Arrange
        //        var expextedResult = _fixture.Build<Product>().CreateMany().ToList();
        //        _mockDbFactory.Setup(x => x.GetProducts()).ReturnsAsync(expextedResult);

        //        //Act
        //        var actualResult = await _productRepository.GetProducts();

        //        //Assert
        //        actualResult.Should().Equal(expextedResult);
        //    }

        //    [Fact]
        //    public async Task GetProductById_ReturnsProduct()
        //    {
        //        //Arrange
        //        var expectedResult = _fixture.Build<Product>().Create();
        //        _mockproductRepository.Setup(x => x.GetProductById(expectedResult.Id)).ReturnsAsync(expectedResult);

        //        //Act
        //        var actualResult = await _productRepository.GetProductById(expectedResult.Id);

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expectedResult);
        //        _mockproductRepository.VerifyAll();
        //    }

        //    [Fact]
        //    public async Task AddProduct_ValidData_ReturnsValidData()
        //    {
        //        //Arrange
        //        var expextedResult = _fixture.Build<Product>().Create();
        //        _mockproductRepository.Setup(x => x.AddProduct(expextedResult)).ReturnsAsync(expextedResult);

        //        //Act
        //        var actualResult = await _productRepository.AddProduct(expextedResult);

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expextedResult);
        //        _mockproductRepository.VerifyAll();
        //    }

        //    [Fact]
        //    public async Task AddProduct_InvalidData_ReturnsNull()
        //    {
        //        //Arrange
        //        productRequest = _fixture.Build<Product>().Create();
        //        _mockproductRepository.Setup(x => x.AddProduct(productRequest)).ReturnsAsync((Product)null);

        //        //Act
        //        var actualResult = await _productRepository.AddProduct(productRequest);
        //        var expectedResult = expectedNullResult;

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expectedResult);
        //        _mockproductRepository.VerifyAll();
        //    }

        //    [Fact]
        //    public async Task UpdateProduct_ValidData_ReturnsValidData()
        //    {
        //        //Arrange
        //        var expectedResult = productRequest = _fixture.Build<Product>().Create();
        //        _mockproductRepository.Setup(x => x.UpdateProduct(productRequest.Id, productRequest)).ReturnsAsync(expectedResult);

        //        //Act
        //        var actualResult = await _productRepository.UpdateProduct(productRequest.Id, productRequest);

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expectedResult);
        //        _mockproductRepository.VerifyAll();
        //    }

        //    [Fact]
        //    public async Task UpdateProduct_InvalidData_ReturnsNull()
        //    {
        //        //Arrange
        //        productRequest = _fixture.Build<Product>().Create();
        //        _mockproductRepository.Setup(x => x.UpdateProduct(productRequest.Id, productRequest)).ReturnsAsync((Product)null);

        //        //Act
        //        var actualResult = await _productRepository.UpdateProduct(productRequest.Id, productRequest);
        //        var expectedResult = expectedNullResult;

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expectedResult);
        //        _mockproductRepository.VerifyAll();
        //    }

        //    [Fact]
        //    public async Task DeleteProduct_GivenValidData_ReturnsOkResult()
        //    {
        //        //Arrange
        //        productRequest = _fixture.Build<Product>().Create();
        //        var expectedResult = productRequest.Id;
        //        _mockproductRepository.Setup(x => x.DeleteProduct(productRequest.Id)).ReturnsAsync(expectedResult);

        //        //Act
        //        var actualResult = await _productRepository.DeleteProduct(productRequest.Id);

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expectedResult);
        //        _mockproductRepository.VerifyAll();
        //    }

        //    [Fact]
        //    public async Task DeleteProductAsync_InvalidData_ReturnsNull()
        //    {
        //        //Arrange
        //        productRequest = _fixture.Build<Product>().Create();
        //        _mockproductRepository.Setup(x => x.DeleteProduct(productRequest.Id)).ReturnsAsync((string)null);

        //        //Act
        //        var actualResult = await _productRepository.DeleteProduct(productRequest.Id);
        //        var expectedResult = expectedNullResult;

        //        //Assert
        //        actualResult.Should().BeEquivalentTo(expectedResult);
        //        _mockproductRepository.VerifyAll();
        //    }
        //}
    }
}