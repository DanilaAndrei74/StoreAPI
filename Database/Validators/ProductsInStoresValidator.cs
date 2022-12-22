using FluentValidation;
using StoreAPI.Contracts.Data.InputModels;

namespace StoreAPI.Database.Validators
{
    public class ProductsInStoresInputValidator : AbstractValidator<ProductInStoreInput>
    {
        public ProductsInStoresInputValidator()
        {
            RuleFor(x => x.ProductId).NotNull()
                                .NotEmpty();
            RuleFor(x => x.StoreId).NotNull()
                                .NotEmpty();
            RuleFor(x => x.Quantity).NotNull()
                                .NotEmpty()
                                .GreaterThanOrEqualTo(0);
        }
    }
}
