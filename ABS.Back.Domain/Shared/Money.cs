namespace ABS.Back.Domain.Shared;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("The Currency is Not Equal");
        }

        return new(first.Amount + second.Amount, first.Currency);
    }

    public static Money operator -(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("The Currency is Not Equal");
        }

        return new(first.Amount - second.Amount, first.Currency);
    }

    public static Money ZeroMoney() => new(0, Currency.None);
    public static Money ZeroMoney(Currency currency) => new(0, currency);


    public bool IsZero() => this == ZeroMoney();
};