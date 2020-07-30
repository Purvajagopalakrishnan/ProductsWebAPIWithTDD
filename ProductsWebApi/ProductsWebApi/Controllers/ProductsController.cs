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
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger _logger;
        private IProductsService _productsService;

        public ProductsController(IProductsService productsService, ILogger logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        [HttpGet]
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

        [Route("{productId}")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(int productId)
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

        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest();
                }
                else
                {
                    await _productsService.UpdateProduct(id, product);
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
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