using FluentValidation;
using StoreAPI.Contracts.Data;

namespace StoreAPI.Database.Validators
{
    public class LoginValidator : AbstractValidator<LoginCredentials>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress()
                                .NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6)
                                .MaximumLength(20);
        }
    }
}
