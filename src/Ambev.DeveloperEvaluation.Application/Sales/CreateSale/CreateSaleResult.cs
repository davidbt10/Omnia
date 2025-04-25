namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        public static CreateSaleResult FromEntity(Domain.Entities.Sale sale)
            => new CreateSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                TotalAmount = sale.TotalAmount
            };
    }
}
