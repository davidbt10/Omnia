﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemCommand>
    {
        public CancelSaleItemValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty();
            RuleFor(x => x.ItemId).NotEmpty();
        }
    }
}
