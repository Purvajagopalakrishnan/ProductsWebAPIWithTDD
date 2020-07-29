using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.DbFactory
{
    public interface IDbFactory
    {
        IDbConnection GetConnection();
    }
}
