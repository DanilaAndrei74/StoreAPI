using FluentValidation;
using StoreAPI.Contracts.Data;

namespace StoreAPI.Database.Validators
{
    public class ProductInputValidator : AbstractValidator<ProductInput>
    {
        public ProductInputValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .MaximumLength(20)
                                .NotEmpty();
            RuleFor(x => x.Description).NotNull()
                                .MaximumLength(200);
        }
    }
}
