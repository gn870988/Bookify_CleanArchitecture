using Domain.Abstractions;

namespace Domain.Bookings.Events;

public record BookingCancelledDomainEvent(BookingId BookingId) : IDomainEvent;