using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;

namespace ProductsWebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _nextProductId = 1;

        public ProductRepository()
        {
            AddProduct(new Product { Id = 1, Name = "Vegetables", Description = "Groceries", Price = 1.20M, Quantity = 2 });
            AddProduct(new Product { Id = 2, Name = "Teddy Bear", Description = "Toys", Price = 3.75M, Quantity = 5 });
            AddProduct(new Product { Id = 3, Name = "Hammer", Description = "Hardware", Price = 16.99M, Quantity = 4 });
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product GetProducts(int productId)
        {
            return products.Find(p => p.Id == productId);
        }

        public Product AddProduct(Product productModel)
        {
            if (productModel == null)
            {
                throw new ArgumentNullException("product is null");
            }
            productModel.Id = _nextProductId++;
            products.Add(productModel);
            return productModel;
        }

        public bool UpdateProduct(Product productModel)
        {
            if (productModel == null)
            {
                throw new ArgumentNullException("product is null");
            }
            int index = products.FindIndex(p => p.Id == productModel.Id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(productModel);
            return true;
        }

        public void DeleteProduct(int productId)
        {
            products.RemoveAll(p => p.Id == productId);
        }
    }
}
