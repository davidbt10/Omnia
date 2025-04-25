using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTest
    {
        private readonly ExternalIdentity _product = new ExternalIdentity(Guid.NewGuid(), "Test Product");

        [Theory]
        [InlineData(1, 100, 0)]   // no discount
        [InlineData(3, 50, 0)]    // no discount
        [InlineData(4, 200, 10)]  // 10% discount
        [InlineData(9, 100, 10)]  // 10% discount
        [InlineData(10, 80, 20)]  // 20% discount
        [InlineData(20, 10, 20)]  // 20% discount
        public void ApplyDiscountRule_CorrectDiscount(int quantity, decimal unitPrice, decimal expectedDiscountPercent)
        {
            // Arrange
            var item = new SaleItem(_product, quantity, unitPrice);

            // Act
            var actualDiscount = item.DiscountPercentage * 100;
            var expectedTotal = quantity * unitPrice * (1 - expectedDiscountPercent / 100);

            // Assert
            Assert.Equal(expectedDiscountPercent, actualDiscount);
            Assert.Equal(expectedTotal, item.TotalAmount);
        }

        [Fact]
        public void ApplyDiscountRule_QuantityAboveMax_ThrowsDomainException()
        {
            // Arrange / Act / Assert
            Assert.Throws<DomainException>(() => new SaleItem(_product, 21, 100));
        }

        [Fact]
        public void Cancel_SetsIsCancelledTrue()
        {
            // Arrange
            var item = new SaleItem(_product, 5, 10);

            // Act
            item.Cancel();

            // Assert
            Assert.True(item.IsCancelled);
        }
    }
}
