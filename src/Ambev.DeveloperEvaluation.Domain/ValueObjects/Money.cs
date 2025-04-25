namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public record Money(decimal Amount, string Currency = "BRL")
    {
        public static implicit operator decimal(Money m) => m.Amount;
        public static Money operator +(Money a, Money b)
            => new(a.Amount + b.Amount, a.Currency);
    }
}
