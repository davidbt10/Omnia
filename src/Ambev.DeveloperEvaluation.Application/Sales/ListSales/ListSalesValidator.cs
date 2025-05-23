﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesValidator : AbstractValidator<ListSalesQuery>
    {
        public ListSalesValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
