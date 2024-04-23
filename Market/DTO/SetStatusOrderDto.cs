using Market.Models;

namespace Market.DTO
{
    public class SetStatusOrderDto
    {
        public Guid IdOrder { get; set; }
        public StatusOrder StatusOrder { get; set; }
    }
}
