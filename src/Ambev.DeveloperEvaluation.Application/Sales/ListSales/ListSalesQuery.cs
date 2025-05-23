﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesQuery : IRequest<ListSalesResult>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
