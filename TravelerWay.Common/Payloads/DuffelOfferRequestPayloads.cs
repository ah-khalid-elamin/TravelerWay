using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TravelerWay.Common.Entities;

namespace TravelerWay.Common.Payloads
{
    public class DuffelOfferReqRequest
    {
        [JsonPropertyName("user_channel_type")]
        public string? ChannelType { get; set; }
        [JsonPropertyName("user_chat_id")]
        public string? ChatId { get; set; } = string.Empty;
        [JsonPropertyName("user_name")]
        public string? Username { get; set; } = string.Empty;
        [JsonPropertyName("user_phone_number")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("user_email")]
        public string? Email { get; set; }
        [JsonPropertyName("user_preferred_language")]
        public string? PreferredLanguage { get; set; }
        [JsonPropertyName("slices")]
        public List<RequestSlice> Slices { get; set; } = new();

        [JsonPropertyName("private_fares")]
        public Dictionary<string, List<PrivateFare>> PrivateFares { get; set; } = new();

        [JsonPropertyName("passengers")]
        public List<Passenger> Passengers { get; set; } = new();

        [JsonPropertyName("max_connections")]
        public int? MaxConnections { get; set; }

        [JsonPropertyName("include_split_ticket")]
        public bool? IncludeSplitTicket { get; set; }

        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; set; }

        [JsonPropertyName("airline_credit_ids")]
        public List<string> AirlineCreditIds { get; set; } = new();
    }

    public class DuffelOfferReqResponse
    {
        [JsonPropertyName("airline_credit_ids")]
        public List<string> AirlineCreditIds { get; set; } = new();

        [JsonPropertyName("offers")]
        public List<DuffelOfferResponse> Offers { get; set; } = new();

        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; set; }

        [JsonPropertyName("live_mode")]
        public bool LiveMode { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("slices")]
        public List<ResponseSlice> Slices { get; set; } = new();

        [JsonPropertyName("passengers")]
        public List<Passenger> Passengers { get; set; } = new();

        [JsonPropertyName("client_key")]
        public string? ClientKey { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class Offer
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("total_currency")]
        public string? TotalCurrency { get; set; }

        // Additional fields can be added as needed to mirror Duffel's offer schema.
    }

    // Placeholder types referenced by the request/response models.
    // If these already exist elsewhere in the project, the definitions here can be removed.
    public class RequestSlice
    {
        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; set; }

        [JsonPropertyName("origin_type")]
        public string? OriginType { get; set; }

        [JsonPropertyName("departure_date")]
        public string? DepartureDate { get; set; }

        [JsonPropertyName("destination")]
        public string? Destination { get; set; }

        [JsonPropertyName("origin")]
        public string? Origin { get; set; }
    }

    public class ResponseSlice
    {
        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; set; }

        [JsonPropertyName("origin_type")]
        public string? OriginType { get; set; }

        [JsonPropertyName("departure_date")]
        public string? DepartureDate { get; set; }

        [JsonPropertyName("destination")]
        public Location? Destination { get; set; }

        [JsonPropertyName("origin")]
        public Location? Origin { get; set; }
    }

    public class PrivateFare
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class Passenger
    {
        [JsonPropertyName("loyalty_programme_accounts")]
        public List<object> LoyaltyProgrammeAccounts { get; set; } = new();

        [JsonPropertyName("fare_type")]
        public string? FareType { get; set; }

        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }

        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("icao_code")]
        public string? IcaoCode { get; set; }

        [JsonPropertyName("city_name")]
        public string? CityName { get; set; }

        [JsonPropertyName("iata_city_code")]
        public string? IataCityCode { get; set; }

        [JsonPropertyName("iata_country_code")]
        public string? IataCountryCode { get; set; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("city")]
        public City? City { get; set; }

        [JsonPropertyName("time_zone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class City
    {
        [JsonPropertyName("icao_code")]
        public string? IcaoCode { get; set; }

        [JsonPropertyName("city_name")]
        public string? CityName { get; set; }

        [JsonPropertyName("iata_city_code")]
        public string? IataCityCode { get; set; }

        [JsonPropertyName("iata_country_code")]
        public string? IataCountryCode { get; set; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("time_zone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}


