using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Please enter the email.")
                .NotNull().WithMessage("Please enter the email.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.");

            RuleFor(c => c.FlagAdmin)
                .NotNull().WithMessage("Please enter the user type.");            

            RuleFor(c => c.FlagActive)
                .NotNull().WithMessage("Please enter the user aticte state.");                          
        }
        
    }
}