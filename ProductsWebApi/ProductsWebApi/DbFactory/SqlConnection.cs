using ProductsWebApi.DbFactory;
using System.Data;

namespace ProductsWebApi.DbFactory
{
    public class SqlConnection : IDbFactory
    {
        public string ConnectionString { get; set; }

        public SqlConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(ConnectionString);
        }
    }
}
