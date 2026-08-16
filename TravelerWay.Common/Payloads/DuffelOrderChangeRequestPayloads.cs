using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{
    public class DuffelOrderChangeReqRequest
    {
        [JsonPropertyName("slices")]
        public DuffelOrderChangeSlices<SliceRemove, SliceAdd>? Slices { get; init; }

        [JsonPropertyName("private_fares")]
        public Dictionary<string, Dictionary<string, string>>? PrivateFares { get; init; } = new Dictionary<string, Dictionary<string, string>>();

        [JsonPropertyName("order_id")]
        public string? OrderId { get; init; }
    }

    public class DuffelOrderChangeSlices<T1,T2>
    {
        [JsonPropertyName("remove")]
        public List<T1>? Remove { get; init; }

        [JsonPropertyName("add")]
        public List<T2>? Add { get; init; }
    }

    public class SliceRemove
    {
        [JsonPropertyName("slice_id")]
        public string? SliceId { get; init; }
    }

    public class SliceAdd
    {
        [JsonPropertyName("origin")]
        public string? Origin { get; init; }

        [JsonPropertyName("destination")]
        public string? Destination { get; init; }

        [JsonPropertyName("departure_date")]
        public string? DepartureDate { get; init; }

        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; init; }
    }



    // Response POCOs for order change request response
    public class DuffelOrderChangeReqResponse
    {
        [JsonPropertyName("updated_at")]
        public string? UpdatedAt { get; init; }

        [JsonPropertyName("slices")]
        public DuffelOrderChangeSlices<SliceRemove, SliceAdd>? Slices { get; init; } // reuse request-level slices

        [JsonPropertyName("order_id")]
        public string? OrderId { get; init; }

        [JsonPropertyName("order_change_offers")]
        public List<DuffelOrderChangeOffer>? OrderChangeOffers { get; init; }

        [JsonPropertyName("live_mode")]
        public bool? LiveMode { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; init; }
    }

    public class DuffelOrderChangeOffer
    {
        [JsonPropertyName("updated_at")]
        public string? UpdatedAt { get; init; }

        [JsonPropertyName("slices")]
        public DuffelOrderChangeSlices<OfferSlice, OfferSlice>? Slices { get; init; }

        [JsonPropertyName("private_fares")]
        public List<OrderChangePrivateFare>? PrivateFares { get; init; }

        [JsonPropertyName("penalty_total_currency")]
        public string? PenaltyTotalCurrency { get; init; }

        [JsonPropertyName("penalty_total_amount")]
        public string? PenaltyTotalAmount { get; init; }

        [JsonPropertyName("order_change_id")]
        public string? OrderChangeId { get; init; }
        [JsonPropertyName("refund_to")]
        public string? RefundTo { get; init; } = "original_payment_method";

        [JsonPropertyName("new_total_currency")]
        public string? NewTotalCurrency { get; init; }

        [JsonPropertyName("new_total_amount")]
        public string? NewTotalAmount { get; init; }

        [JsonPropertyName("live_mode")]
        public bool? LiveMode { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("expires_at")]
        public string? ExpiresAt { get; init; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; init; }

        [JsonPropertyName("conditions")]
        public ChangeConditions? Conditions { get; init; }

        [JsonPropertyName("change_total_currency")]
        public string? ChangeTotalCurrency { get; init; }

        [JsonPropertyName("change_total_amount")]
        public string? ChangeTotalAmount { get; init; }
    }


    public class OfferSlice
    {
        [JsonPropertyName("segments")]
        public List<OrderChangeRequestSegment>? Segments { get; init; }

        [JsonPropertyName("origin_type")]
        public string? OriginType { get; init; }

        [JsonPropertyName("origin")]
        public Location? Origin { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("fare_brand_name")]
        public string? FareBrandName { get; init; }

        [JsonPropertyName("duration")]
        public string? Duration { get; init; }

        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; init; }

        [JsonPropertyName("destination")]
        public Location? Destination { get; init; }
    }

    public class OrderChangeRequestSegment
    {
        [JsonPropertyName("passengers")]
        public List<OrderChangeRequestSegmentPassenger>? Passengers { get; init; }

        [JsonPropertyName("origin_terminal")]
        public string? OriginTerminal { get; init; }

        [JsonPropertyName("origin")]
        public Location? Origin { get; init; }

        [JsonPropertyName("operating_carrier_flight_number")]
        public string? OperatingCarrierFlightNumber { get; init; }

        [JsonPropertyName("operating_carrier")]
        public Carrier? OperatingCarrier { get; init; }

        [JsonPropertyName("marketing_carrier_flight_number")]
        public string? MarketingCarrierFlightNumber { get; init; }

        [JsonPropertyName("marketing_carrier")]
        public Carrier? MarketingCarrier { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("duration")]
        public string? Duration { get; init; }

        [JsonPropertyName("distance")]
        public string? Distance { get; init; }

        [JsonPropertyName("destination_terminal")]
        public string? DestinationTerminal { get; init; }

        [JsonPropertyName("destination")]
        public Location? Destination { get; init; }

        [JsonPropertyName("departing_at")]
        public string? DepartingAt { get; init; }

        [JsonPropertyName("arriving_at")]
        public string? ArrivingAt { get; init; }

        [JsonPropertyName("aircraft")]
        public Aircraft? Aircraft { get; init; }
    }

    public class OrderChangeRequestSegmentPassenger
    {
        [JsonPropertyName("seat")]
        public OrderChangeSeat? Seat { get; init; }

        [JsonPropertyName("passenger_id")]
        public string? PassengerId { get; init; }

        [JsonPropertyName("cabin_class_marketing_name")]
        public string? CabinClassMarketingName { get; init; }

        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; init; }

        [JsonPropertyName("baggages")]
        public List<Baggage>? Baggages { get; init; }
    }

    public class OrderChangeSeat
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("disclosures")]
        public List<string>? Disclosures { get; init; }

        [JsonPropertyName("designator")]
        public string? Designator { get; init; }
    }

    public class Aircraft
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; init; }
    }

    public class OrderChangePrivateFare
    {
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("tracking_reference")]
        public string? TrackingReference { get; init; }

        [JsonPropertyName("tour_code")]
        public string? TourCode { get; init; }

        [JsonPropertyName("corporate_code")]
        public string? CorporateCode { get; init; }
    }

    public class ChangeConditions
    {
        [JsonPropertyName("refund_before_departure")]
        public ConditionDetail? RefundBeforeDeparture { get; init; }

        [JsonPropertyName("change_before_departure")]
        public ConditionDetail? ChangeBeforeDeparture { get; init; }
    }

    public class ConditionDetail
    {
        [JsonPropertyName("penalty_currency")]
        public string? PenaltyCurrency { get; init; }

        [JsonPropertyName("penalty_amount")]
        public string? PenaltyAmount { get; init; }

        [JsonPropertyName("allowed")]
        public bool? Allowed { get; init; }
    }
}
