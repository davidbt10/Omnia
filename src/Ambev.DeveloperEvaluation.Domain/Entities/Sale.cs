using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        private Sale() { }

        public Sale(string saleNumber,
                    DateTime date,
                    ExternalIdentity customer,
                    ExternalIdentity branch,
                    IEnumerable<SaleItem> items)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            Date = date;
            Customer = customer;
            Branch = branch;

            foreach (var item in items)
                AddItem(item);
        }

        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime Date { get; private set; }
        public ExternalIdentity Customer { get; private set; }
        public ExternalIdentity Branch { get; private set; }
        public IReadOnlyCollection<SaleItem> Items => _items;
        public decimal TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }
        private readonly List<SaleItem> _items = new();
        
        public void AddItem(SaleItem item)
        {
            if (_items.Any(i => i.Product.Id == item.Product.Id))
                throw new DomainException($"Item para produto {item.Product.Id} já existe.");

            item.ApplyDiscountRule();
            _items.Add(item);
            RecalculateTotal();
        }

        public void UpdateInfo(Guid customerId, string customerName, Guid branchId, string branchName)
        {
            Customer = new ExternalIdentity(customerId, customerName);
            Branch = new ExternalIdentity(branchId, branchName);
        }

        public void UpdateItem(SaleItem updated)
        {
            var existing = _items.FirstOrDefault(i => i.Id == updated.Id)
                           ?? throw new DomainException("Item não encontrado.");
            existing.UpdateQuantityAndPrice(updated.Quantity, updated.UnitPrice);
            RecalculateTotal();
        }

        public void CancelItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId)
                       ?? throw new DomainException("Item não encontrado.");
            item.Cancel();
            RecalculateTotal();
        }

        public void CancelSale()
        {
            IsCancelled = true;
            foreach (var item in _items) item.Cancel();
            TotalAmount = 0;
        }

        private void RecalculateTotal() =>
            TotalAmount = _items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.TotalAmount);
    }
}
