using Market.Enums;
using Market.Models;

namespace Market.DAL.Interfaces
{
    public interface IProductsRepository
    {
        Task<DbResult> CreateProductAsync(Product product);
        Task<DbResult> DeleteProductAsync(Guid productId);
        Task<DbResult<Product>> GetProductAsync(Guid productId);
        Task<DbResult<IReadOnlyCollection<Product>>> GetProductsAsync(Guid? sellerId = null, string? productName = null, ProductCategory? category = null, int skip = 0, int take = 50);
        IQueryable<Product> GetProductsQueryable();
        Task<DbResult> UpdateProductAsync(Guid productId, ProductUpdateInfo updateInfo);
    }
}