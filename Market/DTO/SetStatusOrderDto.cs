using Market.Models;

namespace Market.DTO
{
    public class SetStatusOrderDto
    {
        public Guid OrderId { get; set; }
        public StatusOrder StatusOrder { get; set; }
    }
}
