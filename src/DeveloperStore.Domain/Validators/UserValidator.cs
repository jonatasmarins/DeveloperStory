using DeveloperStore.Domain.Entities;
using DeveloperStore.Infra.Context.Identity;
using FluentValidation;

namespace DeveloperStore.Domain.Validators
{
    public class UserValidator : AbstractValidator<ApplicationUser>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(CommomMessage.RequiredMessage)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage)
                .EmailAddress().WithMessage(CommomMessage.InvalidMessage);

            RuleFor(x => x.UserName)
                .NotNull().WithMessage(CommomMessage.RequiredMessage)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(CommomMessage.RequiredMessage)
                .NotEmpty().WithMessage(CommomMessage.RequiredMessage);
        }



        //public Name Name { get; set; } = new Name();

        //public Address Address { get; set; } = new Address();

        //public string Phone { get; set; } = string.Empty;

        //[EnumDataType(typeof(Status))]
        //public Status Status { get; set; }

        //[EnumDataType(typeof(Role))]
        //public Role Role { get; set; }

        //public IReadOnlyList<Cart> Carts { get; set; } = [];
    }

    public class NameValidator : AbstractValidator<Name>
    {
    }
}
