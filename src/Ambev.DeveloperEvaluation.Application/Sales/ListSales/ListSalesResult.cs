
namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesResult
    {
        public int TotalItems { get; set; }
        public List<SaleItemDto> Sales { get; set; }
    }
}
