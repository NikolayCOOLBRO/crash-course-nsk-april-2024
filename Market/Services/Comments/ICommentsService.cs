using Market.DTO;
using Market.Models;

namespace Market.Services.Comments
{
    public interface ICommentsService
    {
        Task AddAnonymComment(Guid productId, CreateCommentDto dto);
        Task<IReadOnlyCollection<CommentAnonym>> GetCommentsByProductId(Guid productId);
    }
}