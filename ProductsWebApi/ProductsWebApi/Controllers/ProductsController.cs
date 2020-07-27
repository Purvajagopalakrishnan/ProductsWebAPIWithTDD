using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;

namespace ProductsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private ILogger _logger;
        private IProductsService _productsService;

        public ProductsController(IProductsService productsService, ILogger logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        [HttpGet("/api/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productsService.GetProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [Route("/api/products/{productId}")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(string productId)
        {
            try
            {
                var product = _productsService.GetProductById(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("/api/products")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                await _productsService.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("/api/products/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Product product)
        {
            try
            {
                await _productsService.UpdateProduct(id, product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("/api/products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await _productsService.DeleteProduct(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}