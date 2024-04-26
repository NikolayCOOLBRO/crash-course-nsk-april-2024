namespace Market.Models
{
    public class CommentAnonym
    {
        public const int MaxScore = 5;
        public const int MinScore = 1;

        public Guid Id { get; set; }
        public string Message { get; set; }
        public int Score { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }
    }
}
