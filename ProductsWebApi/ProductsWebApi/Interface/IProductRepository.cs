using ProductsWebApi.Models;
using System.Collections.Generic;

namespace ProductsWebApi.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProducts(int productId);
        Product AddProduct(Product productModel);
        bool UpdateProduct(Product productModel);
        void DeleteProduct(int productId);
    }
}
