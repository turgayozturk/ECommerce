using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Models.Contexts
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Carts> Carts { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
