using ABS.Back.Domain.Abstraction;

namespace ABS.Back.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid Id):IDomainEvent;