using FluentValidation;
using WebApplication1.Contracts;

namespace WebApplication1.Validations
{
    public class MovieValidator : AbstractValidator<MovieRequest>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.PhotoPath)
                .MaximumLength(500).WithMessage("Photo path cannot exceed 500 characters.");

            RuleFor(x => x.Director)
                .NotEmpty().WithMessage("Director is required.")
                .MaximumLength(100).WithMessage("Director cannot exceed 100 characters.");

            RuleFor(x => x.Runtime)
                .InclusiveBetween(1, 600).WithMessage("Runtime must be between 1 and 600 minutes.");
        }
    }
}
