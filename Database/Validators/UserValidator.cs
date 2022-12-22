using FluentValidation;
using StoreAPI.Contracts.Data;

namespace StoreAPI.Database.Validators
{
    public class UserInputValidator : AbstractValidator<UserInput> 
    {
        public UserInputValidator()
        {
            RuleFor(x => x.Email).EmailAddress()
                                .NotEmpty();
            RuleFor(x => x.FirstName).NotNull()
                                .MaximumLength(20)
                                .NotEmpty();
            RuleFor(x => x.LastName).NotNull()
                                .MaximumLength(20)
                                .NotEmpty();
            RuleFor(x => x.Password).NotNull()
                                .MinimumLength(6)
                                .MaximumLength(20);
        }
    }
}
