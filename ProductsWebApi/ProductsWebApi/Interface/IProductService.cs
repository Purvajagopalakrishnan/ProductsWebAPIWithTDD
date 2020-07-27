using ProductsWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsWebApi.Interface
{
    public interface IProductsService
    {
        Task<List<Product>> GetProducts();

        Task<Product> GetProductById(string productId);

        Task<Product> AddProduct(Product productItem);

        Task<Product> UpdateProduct(string id, Product productItem);

        Task<string> DeleteProduct(string id);
    }
}