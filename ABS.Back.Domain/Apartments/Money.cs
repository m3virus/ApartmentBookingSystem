namespace ABS.Back.Domain.Apartments;

public record Money(decimal Amount, Currency currency)
{
    public static Money operator + (Money first, Money second)
    {
        if (first.currency != second.currency)
        {
            throw new InvalidOperationException("The Currency is Not Equal");
        }

        return new(first.Amount + second.Amount, first.currency);
    }

    public static Money operator - (Money first, Money second)
    {
        if (first.currency != second.currency)
        {
            throw new InvalidOperationException("The Currency is Not Equal");
        }

        return new(first.Amount - second.Amount, first.currency);
    }

    public static Money ZeroMoney => new(0, Currency.None);
};