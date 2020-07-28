using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _productItems;

        public ProductRepository()
        {
            _productItems = new List<Product>();
        }

        public async Task<List<Product>> GetProducts()
        {
            return _productItems;
        }

        public async Task<Product> GetProductById(string productId)
        {
            return _productItems.FirstOrDefault(x => x.Id == productId);
        }

        public async Task<Product> AddProduct(Product productItem)
        {
            _productItems.Add(productItem);
            return productItem;
        }

        public async Task<Product> UpdateProduct(string id, Product productItem)
        {
            for (var index = _productItems.Count - 1; index >= 0; index--)
            {
                if (_productItems[index].Id == id)
                {
                    _productItems[index] = productItem;
                }
            }
            return productItem;
        }

        public async Task<string> DeleteProduct(string id)
        {
            for (var index = _productItems.Count - 1; index >= 0; index--)
            {
                if (_productItems[index].Id == id)
                {
                    _productItems.RemoveAt(index);
                }
            }

            return id;
        }
    }
}