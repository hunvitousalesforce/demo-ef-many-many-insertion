using Microsoft.EntityFrameworkCore;

namespace waterapi;

public class WaterContext : DbContext
{
    public WaterContext(DbContextOptions options) : base(options) {}

    public DbSet<Order> Orders {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<OrderDetail> OrderDetails {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity<OrderDetail>();
    }
}
