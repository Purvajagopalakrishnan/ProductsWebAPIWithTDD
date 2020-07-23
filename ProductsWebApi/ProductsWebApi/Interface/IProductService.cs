using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProducts(int productId);
        Product AddProduct(Product productModel);
        bool UpdateProduct(Product productModel);
        void DeleteProduct(int productId);
    }
}
