using carShop.Models;
using Microsoft.EntityFrameworkCore;

namespace carShop.Context
{
    public class CarShopContext : DbContext
    {
        // props for entities
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }

        public CarShopContext([System.Diagnostics.CodeAnalysis.NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
    }
}