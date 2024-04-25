using Market.Models;

namespace Market.DAL.Interfaces
{
    public interface ICartsRepository
    {
        Task<DbResult> AddProductToCartAsync(Guid cartId, Guid productId);
        Task<DbResult> CartClearAllAsync(Guid cartId);
        Task<DbResult<Cart>> GetCartByIdAsync(Guid customerId);
        Task<DbResult> RemoveProductFromCartAsync(Guid cartId, Guid productId);
    }
}