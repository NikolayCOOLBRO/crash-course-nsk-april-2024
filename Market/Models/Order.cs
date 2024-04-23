namespace Market.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SeiledId { get; set; }
        public Guid ProductId { get; set; }
        public StatusOrder Status { get; set; }
    }
}
