namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleResult
    {
        public Guid Id { get; set; }
        public bool IsCancelled { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }

    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}