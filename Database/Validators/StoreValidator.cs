using FluentValidation;
using StoreAPI.Contracts.Data.InputModels;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database.Validators
{
    public class StoreInputValidator : AbstractValidator<StoreInput>
    {
        public StoreInputValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .MaximumLength(20)
                                .NotEmpty();
            RuleFor(x => x.Address).NotEmpty()
                                .NotNull()
                                .MaximumLength(100);
            RuleFor(x => x.Description).NotNull()
                                .MaximumLength(200);
        }
    }
}
