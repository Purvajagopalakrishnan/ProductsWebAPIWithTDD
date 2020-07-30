using Dapper;
using Microsoft.Extensions.Logging;
using ProductsWebApi.Common;
using ProductsWebApi.DbFactory;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System;

namespace ProductsWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly ILogger<ProductRepository> _logger;


        public ProductRepository(IDbFactory dbFactory, ILogger<ProductRepository> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                using (IDbConnection dbConnection = _dbFactory.GetConnection())
                {
                    dbConnection.Open();
                    var products = await dbConnection.QueryAsync<Product>(MySqlQueries.GetProductDataQuery);
                    return products.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }

        public async Task<Product> GetProductById(int productId)
        {
            try
            {
                using (IDbConnection dbConnection = _dbFactory.GetConnection())
                {
                    dbConnection.Open();
                    var products = await dbConnection.QuerySingleAsync<Product>(MySqlQueries.GetProductDataByIDQuery, new { Id = productId});
                    return products;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }

        public async Task<int> AddProduct(Product productItem)
        {
            try
            {
                using (IDbConnection dbConnection = _dbFactory.GetConnection())
                {
                    dbConnection.Open();
                    var productId = await dbConnection.ExecuteScalarAsync<int>(MySqlQueries.AddProductQuery, productItem);
                    return productId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }

        public async Task<int> UpdateProduct(int id, Product productItem)
        {
            try
            {
                var updateProduct = 0;
                using (IDbConnection dbConnection = _dbFactory.GetConnection())
                {
                   dbConnection.Open();
                   updateProduct = await dbConnection.ExecuteAsync(MySqlQueries.UpdateProductDataQuery, productItem);
                }
                return updateProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }

        public async Task<int> DeleteProduct(int id)
        {
            try
            {
                using (IDbConnection dbConnection = _dbFactory.GetConnection())
                {
                    dbConnection.Open();
                    var deleteProduct = await dbConnection.ExecuteAsync(MySqlQueries.DeleteProductDataQuery, new
                    {
                        Id = id
                    }).ConfigureAwait(false);
                    return deleteProduct;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }
    }
}