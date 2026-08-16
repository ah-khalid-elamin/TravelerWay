using TravelerWay.Common.Models;

namespace TravelerWay.Services.Interfaces;

public interface IDuffelService
{
    Task<IReadOnlyList<Flight>> SearchFlightsAsync(SearchFlightRequest request, CancellationToken cancellationToken = default);
    Task<Flight> BookFlightAsync(Guid flightId, BookingRequest request, CancellationToken cancellationToken = default);
}
