using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class RatingValidator : AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(c => c.UserId)
                .NotNull().WithMessage("Please enter the user id.");

            RuleFor(c => c.MovieId)
                .NotNull().WithMessage("Please enter the movie id.");

            RuleFor(c => c.Rate)
                .Must(c => (c >= 0 && c <= 4)).WithMessage("Enter a number between 0 and 4 for the Rate.")
                .NotNull().WithMessage("Please enter the rate for the movie.");                
                     
        }
    }
}