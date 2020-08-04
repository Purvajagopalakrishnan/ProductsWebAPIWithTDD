using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsWebApi.Models;

namespace ProductsWebApi.DBContext
{
    public class ProductsDBContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }

        public ProductsDBContext(DbContextOptions<ProductsDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:SqlConnectionString"]);
        }
    }
}