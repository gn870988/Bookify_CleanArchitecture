using Domain.Abstractions;

namespace Domain.Bookings.Events;

public record BookingConfirmedDomainEvent(BookingId BookingId) : IDomainEvent;