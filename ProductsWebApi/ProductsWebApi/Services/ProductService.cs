﻿using ProductsWebApi.Interface;
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

        public async Task<List<Products>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Products> GetProductById(string productId)
        {
            return await _productRepository.GetProductById(productId);
        }

        public async Task<Products> AddProduct(Products productItem)
        {
            return await _productRepository.AddProduct(productItem);
        }

        public async Task<Products> UpdateProduct(string id, Products productItem)
        {
            return await _productRepository.UpdateProduct(id, productItem);
        }

        public async Task<string> DeleteProduct(string id)
        {
            return await _productRepository.DeleteProduct(id);
        }
    }
}