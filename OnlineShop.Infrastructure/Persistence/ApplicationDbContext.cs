using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Infrastructure.Persistence
{
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
