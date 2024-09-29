namespace ABS.Back.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error NullVallue = new("Error.NullValue", "Null Value Provider");
    public static Error None = new(string.Empty, string.Empty);
}