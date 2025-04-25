using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        private SaleItem() { }

        public SaleItem(ExternalIdentity product, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            Product = product;
            UpdateQuantityAndPrice(quantity, unitPrice);
        }

        public Guid Id { get; private set; }
        public ExternalIdentity Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }


        public void UpdateQuantityAndPrice(int quantity, decimal unitPrice)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            ApplyDiscountRule();
        }

        public void ApplyDiscountRule()
        {
            if (Quantity < 4) DiscountPercentage = 0m;
            else if (Quantity < 10) DiscountPercentage = 0.10m;
            else if (Quantity <= 20) DiscountPercentage = 0.20m;
            else throw new DomainException("Quantidade máxima por item é 20.");

            TotalAmount = Quantity * UnitPrice * (1 - DiscountPercentage);
        }

        public void Cancel() => IsCancelled = true;
    }
}
