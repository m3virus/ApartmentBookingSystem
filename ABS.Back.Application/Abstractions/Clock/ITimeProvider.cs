namespace ABS.Back.Application.Abstractions.Clock;

public interface ITimeProvider
{
    DateTime UtcNow { get; }
}