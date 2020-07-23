using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Product AddProduct(Product productModel)
        {
            return _productRepository.AddProduct(productModel);
        }

        public void DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public Product GetProducts(int productId)
        {
            return _productRepository.GetProducts(productId);
        }

        public bool UpdateProduct(Product productModel)
        {
            return _productRepository.UpdateProduct(productModel);
        }
    }
}
