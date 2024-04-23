using Microsoft.Extensions.Hosting;

namespace Market.Models
{
    public class Cart
    {
        public Guid CustoerId { get; set; }
        public List<Guid> ProductIds { get; set; } = new();
    }
}
