using FluentValidation;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database.Validators
{
    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull()
                                .MaximumLength(30)
                                .MinimumLength(1);
        }
    }
}
