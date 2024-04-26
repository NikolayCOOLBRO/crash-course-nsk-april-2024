using Market.Models;

namespace Market.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<DbResult> ChangeOrderStatusAsync(Guid orderId, StatusOrder newStatusOrder);
        Task<DbResult> CreateOrderAsync(Order order);
        Task<DbResult<IReadOnlyCollection<Order>>> GetOrdersBySeller(Guid sellerId, StatusOrder statusOrder);
    }
}