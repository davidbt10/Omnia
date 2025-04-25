using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTest
    {
        private readonly ExternalIdentity _customer = new ExternalIdentity(Guid.NewGuid(), "Customer A");
        private readonly ExternalIdentity _branch = new ExternalIdentity(Guid.NewGuid(), "Branch A");
        private readonly ExternalIdentity _product = new ExternalIdentity(Guid.NewGuid(), "Product A");
        private readonly ExternalIdentity _productB = new ExternalIdentity(Guid.NewGuid(), "Product B");

        [Fact]
        public void AddItem_SingleItem_CalculatesTotalCorrectly()
        {
            // Arrange
            var sale = new Sale("S001", DateTime.UtcNow, _customer, _branch, new List<SaleItem>());
            var item = new SaleItem(_product, 5, 10);

            // Act
            sale.AddItem(item);

            // Assert
            Assert.Equal(item.TotalAmount, sale.TotalAmount);
        }

        [Fact]
        public void AddItem_DuplicateProduct_ThrowsDomainException()
        {
            // Arrange
            var sale = new Sale("S002", DateTime.UtcNow, _customer, _branch, new List<SaleItem>());
            var item1 = new SaleItem(_product, 2, 20);
            var item2 = new SaleItem(_product, 3, 30);
            sale.AddItem(item1);

            // Act & Assert
            Assert.Throws<DomainException>(() => sale.AddItem(item2));
        }

        [Fact]
        public void CancelItem_SetsItemCancelledAndZeroesTotal()
        {
            // Arrange
            var sale = new Sale("S003", DateTime.UtcNow, _customer, _branch, new List<SaleItem>());
            var item = new SaleItem(_product, 5, 10);
            sale.AddItem(item);

            // Act
            sale.CancelItem(item.Id);

            // Assert
            Assert.True(item.IsCancelled);
            Assert.Equal(0m, sale.TotalAmount);
        }

        [Fact]
        public void CancelSale_SetsAllItemsCancelledAndTotalZero()
        {
            // Arrange
            var sale = new Sale("S004", DateTime.UtcNow, _customer, _branch, new[] { new SaleItem(_product, 2, 10), new SaleItem(_productB, 5, 20) });

            // Act
            sale.CancelSale();

            // Assert
            Assert.True(sale.IsCancelled);
            foreach (var item in sale.Items)
                Assert.True(item.IsCancelled);
            Assert.Equal(0m, sale.TotalAmount);
        }
    }
}
