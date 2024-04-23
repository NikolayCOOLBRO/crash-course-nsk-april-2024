namespace Market.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public StatusOrder Status { get; set; }
    }
}
