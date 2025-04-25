using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
    }
}
