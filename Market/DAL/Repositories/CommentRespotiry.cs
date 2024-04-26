using Market.DAL.Interfaces;
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    public class CommentRespotiry : ICommentRepository
    {
        private readonly IRepositoryContext _repositoryContext;

        public CommentRespotiry(IRepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<DbResult> CreateComment(CommentAnonym comment)
        {
            try
            {
                await _repositoryContext.Comments.AddAsync(comment);
                await _repositoryContext.SaveChangesAsync();

                return DbResult.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult> DeleteComment(CommentAnonym comment)
        {
            try
            {
                _repositoryContext.Comments.Remove(comment);
                await _repositoryContext.SaveChangesAsync();

                return DbResult.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult> EditComment(CommentAnonym newComment)
        {
            var comment = await _repositoryContext.Comments.FirstOrDefaultAsync(item => item.Id.Equals(newComment.Id));

            if (comment == null)
            {
                return DbResult.NotFound();
            }

            comment.Message = newComment.Message;
            comment.Score = newComment.Score;

            await _repositoryContext.SaveChangesAsync();

            return DbResult.Ok();
        }

        public async Task<DbResult<IReadOnlyCollection<CommentAnonym>>> GetCommentsByProductId(Guid productId)
        {
            IQueryable<CommentAnonym> comments = _repositoryContext.Comments.Where(item => item.ProductId.Equals(productId));

            var list = await comments.ToListAsync();

            return DbResult<IReadOnlyCollection<CommentAnonym>>.Ok(list);
        }
    }
}
