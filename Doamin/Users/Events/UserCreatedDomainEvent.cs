using Domain.Abstractions;

namespace Domain.Users.Events;

public record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;