using ProductsWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsWebApi.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();

        Task<Product> GetProductById(int productId);

        Task<int> AddProduct(Product productItem);

        Task<int> UpdateProduct(int id, Product productItem);

        Task<int> DeleteProduct(int id);
    }
}