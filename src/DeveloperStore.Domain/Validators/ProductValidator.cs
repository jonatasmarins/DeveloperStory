using DeveloperStore.Domain.Entities;
using FluentValidation;

namespace DeveloperStore.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage)
                .NotNull().WithMessage(CommomMessage.RequiredMessage);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage)
                .NotNull().WithMessage(CommomMessage.RequiredMessage)
                .GreaterThan(0).WithMessage(CommomMessage.GreaterThanMessage);

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage)
                .NotNull().WithMessage(CommomMessage.RequiredMessage);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage)
                .NotNull().WithMessage(CommomMessage.RequiredMessage);

            RuleFor(x => x.Rating)
                .NotNull().WithMessage(CommomMessage.RequiredMessage)
                .SetValidator(new RatingValidator());
        }
    }
}
