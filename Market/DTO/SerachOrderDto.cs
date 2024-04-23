using Market.Models;

namespace Market.DTO
{
    public class SerachOrderDto
    {
        public StatusOrder StatusOrder { get; set; } = StatusOrder.None;

        public Guid SellerId { get; set; }
    }
}
