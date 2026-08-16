using System.Net.Http.Json;
using TravelerWay.Common.Models;
using TravelerWay.Services.Interfaces;

namespace TravelerWay.Services.Services;

public class DuffelService : IDuffelService
{
    private readonly HttpClient _httpClient;

    public DuffelService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<Flight>> SearchFlightsAsync(SearchFlightRequest request, CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            data = new
            {
                origin = request.Origin,
                destination = request.Destination,
                departure_date = request.DepartureDate.ToString("yyyy-MM-dd")
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/offers/search", payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: cancellationToken);
        return new List<Flight>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Origin = request.Origin,
                Destination = request.Destination,
                DepartureTime = request.DepartureDate.AddHours(8),
                ArrivalTime = request.DepartureDate.AddHours(14),
                Airline = "Duffel Sample",
                FlightNumber = "DUF-101",
                Price = 299m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            }
        };
    }

    public Task<Flight> BookFlightAsync(Guid flightId, BookingRequest request, CancellationToken cancellationToken = default)
    {
        var flight = new Flight
        {
            Id = flightId,
            Origin = request.Origin,
            Destination = request.Destination,
            DepartureTime = DateTime.UtcNow.AddDays(1).AddHours(8),
            ArrivalTime = DateTime.UtcNow.AddDays(1).AddHours(14),
            Airline = "Duffel Sample",
            FlightNumber = "DUF-101",
            Price = 299m,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        return Task.FromResult(flight);
    }
}
