using Market.Models;

namespace Market.DAL.Interfaces
{
    public interface ICommentRepository
    {
        Task<DbResult> CreateComment(CommentAnonym comment);
        Task<DbResult> EditComment(CommentAnonym comment);
        Task<DbResult<IReadOnlyCollection<CommentAnonym>>> GetCommentsByProductId(Guid id);
        Task<DbResult> DeleteComment(CommentAnonym comment);
    }
}
