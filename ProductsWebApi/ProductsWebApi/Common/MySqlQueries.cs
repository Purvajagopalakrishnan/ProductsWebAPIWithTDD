using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.Common
{
    public class MySqlQueries
    {
        public const string GetProductDataQuery = @"SELECT * FROM Products";

        public const string GetProductDataByIDQuery = @"SELECT Id, Name, Description, Quantity, Price
        FROM Products WHERE Id=@Id ";
    
        public const string AddProductQuery = @"INSERT INTO Products (Name, Description, Quantity, Price) VALUES(@Name, @Description, @Quantity, @Price); SELECT max(Id) from Products ";
    
        public const string UpdateProductDataQuery = @"UPDATE Products SET Name=@Name, Description=@Description, Quantity=@Quantity, Price=@Price WHERE Id=@Id";

        public const string DeleteProductDataQuery = @"DELETE FROM Products WHERE Id=@Id";

    }
}
