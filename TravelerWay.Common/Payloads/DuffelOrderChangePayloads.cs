using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{
    public class DuffelOrderChange
    {
        [JsonPropertyName("selected_services")]
        public List<object>? SelectedServices { get; set; }

        [JsonPropertyName("penalty_total_currency")]
        public string? PenaltyTotalCurrency { get; set; }

        [JsonPropertyName("penalty_total_amount")]
        public string? PenaltyTotalAmount { get; set; }

        [JsonPropertyName("available_payment_types")]
        public List<string>? AvailablePaymentTypes { get; set; }

        [JsonPropertyName("new_total_currency")]
        public string? NewTotalCurrency { get; set; }

        [JsonPropertyName("new_total_amount")]
        public string? NewTotalAmount { get; set; }

        [JsonPropertyName("refund_to")]
        public string? RefundTo { get; set; }

        [JsonPropertyName("change_total_currency")]
        public string? ChangeTotalCurrency { get; set; }

        [JsonPropertyName("change_total_amount")]
        public string? ChangeTotalAmount { get; set; }

        [JsonPropertyName("confirmed_at")]
        public DateTimeOffset? ConfirmedAt { get; set; }

        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        [JsonPropertyName("live_mode")]
        public bool LiveMode { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("slices")]
        public DuffelOrderChangeSlices<DuffelOrderChangeSlice, DuffelOrderChangeSlice>? Slices { get; init; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTimeOffset? ExpiresAt { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelOrderChangeRequest
    {
        [JsonPropertyName("selected_order_change_offer")]
        public string? SelectedOrderChangeOfferId { get; set; }
    }

    public class DuffelOrderChangeSlice
    {
        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; set; }

        [JsonPropertyName("origin_type")]
        public string? OriginType { get; set; }

        [JsonPropertyName("fare_brand_name")]
        public string? FareBrandName { get; set; }

        [JsonPropertyName("segments")]
        public List<DuffelOrderChangeSegment>? Segments { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("destination")]
        public DuffelOrderChangeLocation? Destination { get; set; }

        [JsonPropertyName("origin")]
        public DuffelOrderChangeLocation? Origin { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelOrderChangeSegment
    {
        [JsonPropertyName("origin_terminal")]
        public string? OriginTerminal { get; set; }

        [JsonPropertyName("destination_terminal")]
        public string? DestinationTerminal { get; set; }

        [JsonPropertyName("aircraft")]
        public DuffelOrderChangeAircraft? Aircraft { get; set; }

        [JsonPropertyName("departing_at")]
        public DateTimeOffset? DepartingAt { get; set; }

        [JsonPropertyName("arriving_at")]
        public DateTimeOffset? ArrivingAt { get; set; }

        [JsonPropertyName("operating_carrier")]
        public DuffelOrderChangeCarrier? OperatingCarrier { get; set; }

        [JsonPropertyName("marketing_carrier")]
        public DuffelOrderChangeCarrier? MarketingCarrier { get; set; }

        [JsonPropertyName("operating_carrier_flight_number")]
        public string? OperatingCarrierFlightNumber { get; set; }

        [JsonPropertyName("marketing_carrier_flight_number")]
        public string? MarketingCarrierFlightNumber { get; set; }

        [JsonPropertyName("passengers")]
        public List<DuffelOrderChangePassenger>? Passengers { get; set; }

        [JsonPropertyName("distance")]
        public string? Distance { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("destination")]
        public DuffelOrderChangeLocation? Destination { get; set; }

        [JsonPropertyName("origin")]
        public DuffelOrderChangeLocation? Origin { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelOrderChangeAircraft
    {
        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelOrderChangeCarrier
    {
        [JsonPropertyName("logo_symbol_url")]
        public string? LogoSymbolUrl { get; set; }

        [JsonPropertyName("logo_lockup_url")]
        public string? LogoLockupUrl { get; set; }

        [JsonPropertyName("conditions_of_carriage_url")]
        public string? ConditionsOfCarriageUrl { get; set; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelOrderChangePassenger
    {
        [JsonPropertyName("baggages")]
        public List<DuffelOrderChangeBaggage>? Baggages { get; set; }

        [JsonPropertyName("cabin_class_marketing_name")]
        public string? CabinClassMarketingName { get; set; }

        [JsonPropertyName("passenger_id")]
        public string? PassengerId { get; set; }

        [JsonPropertyName("seat")]
        public string? Seat { get; set; }

        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; set; }
    }

    public class DuffelOrderChangeBaggage
    {
        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class DuffelOrderChangeLocation
    {
        [JsonPropertyName("iata_country_code")]
        public string? IataCountryCode { get; set; }

        [JsonPropertyName("iata_city_code")]
        public string? IataCityCode { get; set; }

        [JsonPropertyName("city_name")]
        public string? CityName { get; set; }

        [JsonPropertyName("icao_code")]
        public string? IcaoCode { get; set; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("city")]
        public DuffelOrderChangeCity? City { get; set; }

        [JsonPropertyName("time_zone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelOrderChangeCity
    {
        [JsonPropertyName("iata_country_code")]
        public string? IataCountryCode { get; set; }

        [JsonPropertyName("iata_city_code")]
        public string? IataCityCode { get; set; }

        [JsonPropertyName("city_name")]
        public string? CityName { get; set; }

        [JsonPropertyName("icao_code")]
        public string? IcaoCode { get; set; }

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
