using DeveloperStore.Domain.Entities;
using FluentValidation;

namespace DeveloperStore.Domain.Validators
{
    public class RatingValidator : AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().WithMessage(CommomMessage.RequiredMessage);
        }
    }
}
