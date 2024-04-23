using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    internal sealed class CartsRepository
    {
        private readonly RepositoryContext _context;

        public CartsRepository()
        {
            _context = new RepositoryContext();
        }

        public async Task<DbResult<Cart>> GetCartByIdAsync(Guid customerId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(item => item.CustoerId.Equals(customerId));

            return cart != null 
                ? DbResult<Cart>.Of(cart, DbResultStatus.Ok) :
                DbResult<Cart>.Of(null!, DbResultStatus.NotFound);
        }

        public async Task<DbResult> AddProductToCartAsync(Guid cartId, Guid productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(item => item.CustoerId.Equals(cartId));

            if (cart == null)
            {
                return DbResult.NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(item => item.Id.Equals(productId));

            if (product == null)
            {
                return DbResult.NotFound();
            }

            try
            {
                cart.ProductIds = new List<Guid>(cart.ProductIds) { productId };
                await _context.SaveChangesAsync();

                return DbResult.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult> RemoveProductFromCartAsync(Guid cartId, Guid productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(item => item.CustoerId.Equals(cartId));

            if (cart == null)
            {
                return DbResult.NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(item => item.Id.Equals(productId));

            if (product == null)
            {
                return DbResult.NotFound();
            }

            try
            {
                cart.ProductIds = new List<Guid>(cart.ProductIds);
                cart.ProductIds.Remove(productId);
                await _context.SaveChangesAsync();

                return DbResult.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult> CartClearAllAsync(Guid cartId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(item => item.CustoerId.Equals(cartId));

            if (cart == null)
            {
                return DbResult.NotFound();
            }

            try
            {
                cart.ProductIds = new List<Guid>();
                await _context.SaveChangesAsync();

                return DbResult.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }
    }
}
