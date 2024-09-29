using ABS.Back.Domain.Abstractions;

namespace ABS.Back.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid Id):IDomainEvent;