using ProductsWebApi.DbFactory;
using System.Data;

namespace ProductsWebApi.DbFactory
{
    public class MySqlConnection : IDbFactory
    {
        public string ConnectionString { get; set; }

        public MySqlConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(ConnectionString);
        }
    }
}
