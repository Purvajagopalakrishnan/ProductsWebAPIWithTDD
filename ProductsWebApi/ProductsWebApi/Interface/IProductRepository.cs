using ProductsWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsWebApi.Interface
{
    public interface IProductRepository
    {
        Task<List<Products>> GetProducts();

        Task<Products> GetProductById(string productId);

        Task<Products> AddProduct(Products productItem);

        Task<Products> UpdateProduct(string id, Products productItem);

        Task<string> DeleteProduct(string id);
    }
}