using FluentValidation;
using Market.DTO;
using Market.Models;

namespace Market.UseCases.Validators.Comments
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator() 
        {
            RuleFor(item => item.Score)
                .LessThanOrEqualTo(CommentAnonym.MaxScore)
                .GreaterThanOrEqualTo(CommentAnonym.MinScore);

            RuleFor(item => item.Message)
                .NotEmpty()
                .NotNull();

        }
    }
}
