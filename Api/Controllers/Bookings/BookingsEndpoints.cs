﻿using Application.Bookings.GetBooking;
using Application.Bookings.ReserveBooking;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Controllers.Bookings;

public static class BookingsEndpoints
{
    public static IEndpointRouteBuilder MapBookingsEndpoints(this IEndpointRouteBuilder builder)
    {
        var routeGroupBuilder = builder.MapGroup("api/bookings").RequireAuthorization();

        routeGroupBuilder.MapGet("{id}", GetBooking);

        routeGroupBuilder.MapPost("", ReserveBooking);

        return builder;
    }

    public static async Task<Results<Ok<BookingResponse>, NotFound>> GetBooking(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);

        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
    }

    public static async Task<Results<CreatedAtRoute<Guid>, BadRequest<Error>>> ReserveBooking(
        ReserveBookingRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(
            request.ApartmentId,
            request.UserId,
            request.StartDate,
            request.EndDate);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.CreatedAtRoute(result.Value, nameof(GetBooking), new { id = result.Value });
    }
}