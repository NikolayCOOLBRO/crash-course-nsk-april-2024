using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL;

public sealed class RepositoryContext : DbContext, IRepositoryContext
{
    public RepositoryContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();
    public DbSet<CommentAnonym> Comments => Set<CommentAnonym>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Properties"))
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(DataInitializer.InitializeProduct());
        modelBuilder.Entity<Cart>().HasKey(item => item.CustoerId);
        modelBuilder.Entity<Cart>().HasData(DataInitializer.InitalizeCart());
        modelBuilder.Entity<Cart>().Property(c => c.ProductIds).HasColumnType("TEXT")
            .HasConversion(
                ids => string.Join(';', ids),
                s => s.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList());

        modelBuilder.Entity<User>().HasData(DataInitializer.InitalizeSuperUser());
    }
}