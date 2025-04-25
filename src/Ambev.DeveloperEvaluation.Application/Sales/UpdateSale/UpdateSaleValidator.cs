using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemDtoValidator());
        }
    }

    public class UpdateSaleItemDtoValidator : AbstractValidator<UpdateSaleItemDto>
    {
        public UpdateSaleItemDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
        }
    }
}
