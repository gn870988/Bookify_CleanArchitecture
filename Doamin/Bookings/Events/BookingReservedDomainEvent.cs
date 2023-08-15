using Domain.Abstractions;

namespace Domain.Bookings.Events;

public record BookingReservedDomainEvent(BookingId BookingId) : IDomainEvent;