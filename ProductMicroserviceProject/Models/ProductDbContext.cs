using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProductMicroserviceProject.Models
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
