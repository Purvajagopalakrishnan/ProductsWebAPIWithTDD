using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Common
{
    public class MySqlQueries
    {
        public const string GetProductDataQuery = "SELECT P.Id, P.Name, P.Description, P.Quantity, P.Price " +
      "FROM Products P ";

        public const string GetProductDataByIDQuery = GetProductDataQuery + " WHERE P.Id=@Id ";
    
        public const string AddProductQuery = "INSERT INTO Products(Name, Description, Quantity, Price)" +
      " VALUES(@Name, @Description, @Quantity, @Price); SELECT LAST_INSERT_ID();";
    
        public const string UpdateProductDataQuery = "UPDATE Products SET Name=@Name, Description=@Description, Quantity=@Quantity, Price=@Price WHERE Id=@Id";

        public const string DeleteProductDataQuery = "DELETE FROM Products WHERE Id=@Id; ";

    }
}
