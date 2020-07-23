

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsWebApi.Controllers;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using ProductsWebApi.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProductsWebApi.Tests
{
    public class ProductsTest
    {
        [Fact]
        public void GetAllProducts_IfValid_ReturnsAllProduct()
        {
            var productService = new Mock<IProductService>();
            var logger = new Mock<ILogger<ProductsController>>();
            List<Product> product = new List<Product>()
            {
                new Product { Id = 1, Name = "Vegetables", Description = "Groceries", Price = 1.20M, Quantity = 2 },
                new Product { Id = 2, Name = "Teddy Bear", Description = "Toys", Price = 3.75M, Quantity = 5 },
                new Product { Id = 3, Name = "Hammer", Description = "Hardware", Price = 16.99M, Quantity = 4 }
            };

            productService.Setup(x => x.GetProducts()).Returns(product);
            var controller = new ProductsController(productService.Object,logger.Object);

            var actualResult = controller.GetAllProducts();
            Assert.NotNull(actualResult);
            Assert.Equal(product, actualResult);
        }

        [Fact]
        public void GetProductById_IfValid_ReturnsProduct()
        {
            var productService = new Mock<IProductService>();
            var logger = new Mock<ILogger<ProductsController>>();
            var productId = 1;
            var product = new Product();
            productService.Setup(x => x.GetProducts(productId)).Returns(product);
           
            var controller = new ProductsController(productService.Object, logger.Object);

            var actualResult = controller.GetProduct(productId);
            Assert.NotNull(actualResult);
            Assert.Equal(product, actualResult);
            Assert.IsType<Product>(actualResult);
        }

        [Fact]
        public void Post_ReturnsCreatedResult_WhenObjectIsPassed()
        {
            var productService = new Mock<IProductService>();
            var logger = new Mock<ILogger<ProductsController>>();
            
            var product = new Product();
            productService.Setup(x => x.AddProduct(It.Is<Product>(x => x.Id == 4 && x.Name == "Eyeliner" && x.Price == 3M && x.Quantity == 2 && x.Description == "Cosmetics"))).Returns(product);

            var controller = new ProductsController(productService.Object, logger.Object);

            var actualResult = controller.AddProduct(
            new Product()
            {
                Id = 4,
                Name = "Eyeliner",
                Price = 3M,
                Quantity = 2,
                Description = "Cosmetics"
            });
            Assert.NotNull(actualResult);
            Assert.Equal(product, actualResult);
            Assert.IsType<Product>(actualResult);
        }
        
        [Fact]
        public void Task_DeleteProduct_ReturnsOkResult()
        {
            var productService = new Mock<IProductService>();
            var logger = new Mock<ILogger<ProductsController>>();
            var productId = 1;
            productService.Setup(x => x.DeleteProduct(productId));

            var controller = new ProductsController(productService.Object, logger.Object);

            var okResponse = controller.DeleteProduct(productId);

            Assert.IsType<OkResult>(okResponse);
        }
        
        [Fact]
        public void DeleteProduct_IfNotExisitngProductPassed_ReturnsNotFoundResponse()
        {
            var productService = new Mock<IProductService>();
            var logger = new Mock<ILogger<ProductsController>>();
            var productId = 12;
            productService.Setup(x => x.DeleteProduct(productId));

            var controller = new ProductsController(productService.Object, logger.Object);

            var badResponse = controller.DeleteProduct(productId);

            Assert.IsType<NotFoundResult>(badResponse);
        }
    }
}
