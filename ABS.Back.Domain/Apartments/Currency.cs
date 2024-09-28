namespace ABS.Back.Domain.Apartments;

public record Currency
{
    internal static Currency None = new("");
    public static Currency Usd = new("USD");
    public static Currency Rial = new("Rial");

    private Currency (string code) => code = Code;
    
    public string Code { get; init; } = null!; 

    public static Currency Type(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
               throw new ApplicationException("The Currency is Not Valid");

    }

    public static IReadOnlyList<Currency> All = new[]
    {
        Usd,
        Rial,
    };
};