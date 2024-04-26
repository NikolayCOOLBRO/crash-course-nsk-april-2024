using FluentValidation;
using Market.DAL.Interfaces;
using Market.DTO;
using Market.Misc;
using Market.Models;
using Market.Services.Users;
using Market.UseCases.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Market.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentRepository _repositoryComment;
        private readonly IValidator<CreateCommentDto> _validatorCreateComment;

        public CommentsService(ICommentRepository commentRepository,
                                IValidator<CreateCommentDto> validatorCreateComment)
        {
            _repositoryComment = commentRepository;
            _validatorCreateComment = validatorCreateComment;
        }

        public async Task AddAnonymComment(Guid productId, CreateCommentDto dto)
        {
            var validationResult = _validatorCreateComment.Validate(dto);

            if (!validationResult.IsValid)
            {
                var message = new List<string>();

                foreach (var item in validationResult.Errors)
                {
                    message.Add(item.ErrorMessage);
                }

                var errorDetails = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Messages = message
                };

                throw new InvalidDataRequestException(errorDetails);
            }

            var newComment = new CommentAnonym()
            {
                Id = Guid.NewGuid(),
                Score = dto.Score,
                Message = dto.Message,
                ProductId = productId
            };

            await _repositoryComment.CreateComment(newComment);
        }


        public async Task<IReadOnlyCollection<CommentAnonym>> GetCommentsByProductId(Guid productId)
        {
            var products = await _repositoryComment.GetCommentsByProductId(productId);

            return products.Result;
        }
    }
}
