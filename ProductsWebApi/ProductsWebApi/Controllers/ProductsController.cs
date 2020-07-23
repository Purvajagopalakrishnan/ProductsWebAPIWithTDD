using System;
using System.Collections.Generic;
using System.Linq;
using ProductsWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ProductsWebApi.Interface;
using System.Web.Http;
using System.Net;

namespace ProductsWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Microsoft.AspNetCore.Mvc.Route("/api/getProducts/{productId:int}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Product GetProduct(int productId)
        {
            try
            {
                Product product = _productService.GetProducts(productId);
                if (product == null)
                {
                    throw new Exception();
                }
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw new HttpResponseException(HttpStatusCode.InternalServerError); ;
            }
        }
        [Microsoft.AspNetCore.Mvc.Route("/api/getProducts")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _productService.GetProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Microsoft.AspNetCore.Mvc.Route("/api/addProduct")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Product AddProduct([Microsoft.AspNetCore.Mvc.FromBody] Product productModel)
        {
            try
            {
                var product = _productService.AddProduct(productModel);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw new HttpResponseException(HttpStatusCode.InternalServerError); ;
            }
        }

        [Microsoft.AspNetCore.Mvc.Route("api/deleteProduct/{productId:int}")]
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public ActionResult DeleteProduct(int productId)
        {
            Product product = _productService.GetProducts(productId);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _productService.DeleteProduct(productId);
            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.Route("{productId:int}")]
        [Microsoft.AspNetCore.Mvc.HttpPut]
        public void UpdateProduct(int productId, [Microsoft.AspNetCore.Mvc.FromBody] Product productModel)
        {
            try
            {
                productModel.Id = productId;
                if (!_productService.UpdateProduct(productModel))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw new HttpResponseException(HttpStatusCode.InternalServerError); ;
            }
        }
    }
}