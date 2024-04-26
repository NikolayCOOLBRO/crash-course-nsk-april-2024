using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL
{
    public interface IRepositoryContext
    {
        DbSet<Product> Products { get; }
        DbSet<Cart> Carts { get; }
        DbSet<Order> Orders { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
