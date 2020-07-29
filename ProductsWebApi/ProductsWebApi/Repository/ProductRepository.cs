using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using ProductsWebApi.Common;
using ProductsWebApi.DbFactory;
using ProductsWebApi.Interface;
using ProductsWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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
                    var products = await dbConnection.QueryAsync<Product>(MySqlQueries.GetProductDataQuery);
                    return products.ToList();
                }
            }
            catch (MySqlException ex)
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
                    var products = await dbConnection.QueryAsync<Product>(MySqlQueries.GetProductDataByIDQuery);
                    return products.SingleOrDefault(x => x.Id == productId);
                }
            }
            catch (MySqlException ex)
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
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var productId = await dbConnection.QuerySingleOrDefaultAsync<int>(MySqlQueries.AddProductQuery, new
                        {
                            productItem.Name,
                            productItem.Description,
                            productItem.Quantity,
                            productItem.Price
                        });

                        transaction.Complete();
                        return productId;
                    }
                }
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }

        public async Task<int> UpdateProduct(int id, Product productItem)
        {
            try
            {
                using (IDbConnection dbConnection = _dbFactory.GetConnection())
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        int updateProduct = 0;
                        if (productItem != null)
                        {
                            updateProduct = await dbConnection.ExecuteAsync(MySqlQueries.UpdateProductDataQuery, new
                            {
                                productItem.Id,
                                productItem?.Name,
                                productItem?.Description,
                                productItem?.Quantity,
                                productItem?.Price
                            });
                        }

                        transaction.Complete();
                        return updateProduct;
                    }
                }
            }
            catch (MySqlException ex)
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
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var deleteProduct = await dbConnection.ExecuteAsync(MySqlQueries.DeleteProductDataQuery, new
                        {
                            Id = id
                        }).ConfigureAwait(false);
                        transaction.Complete();
                        return deleteProduct;
                    }
                }
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }
    }
}