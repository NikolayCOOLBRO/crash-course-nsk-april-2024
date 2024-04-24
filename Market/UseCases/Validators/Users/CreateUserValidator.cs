using FluentValidation;
using Market.DTO;
using System.Text.RegularExpressions;

namespace Market.UseCases.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8);
            RuleFor(x => x.Login).NotEmpty().NotNull().MinimumLength(2);
        }
    }
}
