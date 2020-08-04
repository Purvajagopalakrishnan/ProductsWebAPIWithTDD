using Microsoft.EntityFrameworkCore;
using ProductsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsWebApi.DBContext
{
    public interface IProductsDBContext
    {
        DbSet<Products> Products { get; set; }
        DbSet<Users> Users { get; set; }
    }
}
