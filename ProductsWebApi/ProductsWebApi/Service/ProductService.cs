using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsWebApi.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productRepository;

        public ProductsService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _productRepository.GetProductById(productId);
        }

        public async Task<int> AddProduct(Product productItem)
        {
            return await _productRepository.AddProduct(productItem);
        }

        public async Task<int> UpdateProduct(int id, Product productItem)
        {
            return await _productRepository.UpdateProduct(id, productItem);
        }

        public async Task<int> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }
    }
}