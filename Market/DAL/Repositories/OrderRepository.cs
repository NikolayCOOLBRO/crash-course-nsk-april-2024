using Market.DAL.Interfaces;
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    internal sealed class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _context;

        public OrderRepository()
        {
            _context = new RepositoryContext();
        }

        public async Task<DbResult> CreateOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return DbResult.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult> ChangeOrderStatusAsync(Guid orderId, StatusOrder newStatusOrder)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(item => item.Id.Equals(orderId));

            if (order == null)
            {
                return DbResult.NotFound();
            }

            try
            {
                order.Status = newStatusOrder;
                await _context.SaveChangesAsync();
                return DbResult.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult<IReadOnlyCollection<Order>>> GetOrdersBySeller(Guid sellerId, StatusOrder statusOrder)
        {
            var query = _context.Orders.Where(item => item.SeiledId.Equals(sellerId) && item.Status.Equals(statusOrder));

            var orders = await query.ToListAsync();
            return DbResult<IReadOnlyCollection<Order>>.Ok(orders);
        }
    }
}
