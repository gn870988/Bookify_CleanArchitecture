﻿using Domain.Abstractions;

namespace Domain.Bookings.Events;

public record BookingCompletedDomainEvent(BookingId BookingId) : IDomainEvent;