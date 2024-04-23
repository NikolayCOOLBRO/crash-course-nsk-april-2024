namespace Market.DTO
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public Guid CartId { get; set; }
        public Guid SeildId { get; set; }
        public Guid ProductId { get; set; }
    }
}
