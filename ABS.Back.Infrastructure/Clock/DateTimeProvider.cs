using ABS.Back.Application.Abstractions.Clock;
using Microsoft.Extensions.Primitives;

namespace ABS.Back.Infrastructure.Clock
{
    internal sealed class DateTimeProvider : ITimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
