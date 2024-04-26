using Market.Models;

namespace Market.DTO
{
    public class GetCommentDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string Message { get; set; }

        public static GetCommentDto FromModel(CommentAnonym commentAnonym)
        {
            return new GetCommentDto()
            {
                Id = commentAnonym.Id,
                Score = commentAnonym.Score,
                Message = commentAnonym.Message
            };
        }
    }
}
