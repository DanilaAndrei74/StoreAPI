using FluentValidation;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database.Validators
{
    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
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
