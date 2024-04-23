namespace Market.Models
{
    public class Cart
    {
        public Guid CustoerId { get; set; }
        public Product Product { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
