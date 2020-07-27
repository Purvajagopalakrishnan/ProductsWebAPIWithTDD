using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsWebApi.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsService _productsService;

        public ProductsService(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productsService.GetProducts();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _productsService.GetProductById(productId);
        }

        public async Task<Product> AddProduct(Product productItem)
        {
            return await _productsService.AddProduct(productItem);
        }

        public async Task<Product> UpdateProduct(string id, Product productItem)
        {
            return await _productsService.UpdateProduct(id, productItem);
        }

        public async Task<string> DeleteProduct(string id)
        {
            return await _productsService.DeleteProduct(id);
        }
    }
}