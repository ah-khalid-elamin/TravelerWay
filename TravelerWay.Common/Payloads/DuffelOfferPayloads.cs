using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{
    public class DuffelOfferResponse
    {

        [JsonPropertyName("total_emissions_kg")]
        public string? TotalEmissionsKg { get; set; }
        [JsonPropertyName("available_airline_credit_ids")]
        public List<string>? AvailableAirlineCreditIds { get; set; }
        [JsonPropertyName("available_services")]
        public List<DuffelOfferService>? AvailableServices { get; set; }
        [JsonPropertyName("intended_services")]
        public List<IntendedService>? IntendedServices { get; set; }
        [JsonPropertyName("intended_payment_methods")]
        public List<PaymentMethod>? IntendedPaymentMethods  { get; set; }
        [JsonPropertyName("payment_requirements")]
        public PaymentRequirements? PaymentRequirements { get; set; }
        [JsonPropertyName("supported_passenger_identity_document_types")]
        public List<string>? SupportedPassengerIdentityDocumentTypes { get; set; }
        [JsonPropertyName("passenger_identity_documents_required")]
        public bool PassengerIdentityDocumentsRequired { get; set; }
        [JsonPropertyName("tax_currency")]
        public string? TaxCurrency { get; set; }
        [JsonPropertyName("supported_loyalty_programmes")]
        public List<object>? SupportedLoyaltyProgrammes { get; set; }
        [JsonPropertyName("private_fares")]
        public List<object>? PrivateFares { get; set; }
        [JsonPropertyName("tax_amount")]
        public string? TaxAmount { get; set; }
        [JsonPropertyName("base_currency")]
        public string? BaseCurrency { get; set; }
        [JsonPropertyName("base_amount")]
        public string? BaseAmount { get; set; }
        [JsonPropertyName("total_currency")]
        public string? TotalCurrency { get; set; }
        [JsonPropertyName("live_mode")]
        public bool LiveMode { get; set; }
        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("slices")]
        public List<Slice>? Slices { get; set; }
        [JsonPropertyName("passengers")]
        public List<DuffelOfferPassenger>? Passengers { get; set; }
        [JsonPropertyName("conditions")]
        public DuffelConditions? Conditions { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }
        [JsonPropertyName("partial")]
        public bool Partial { get; set; }
        [JsonPropertyName("owner")]
        public Carrier? Owner { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }


    public class DuffelOfferPassenger
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("infant_passenger_id")]
        public string? InfantPassengerId { get; set; }

        [JsonPropertyName("identity_documents")]
        public List<DuffelOrderRequestIdentityDocument> IdentityDocuments { get; set; } = new();

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("born_on")]
        public string? BornOn { get; set; }
    }
    public class DuffelOfferPricingRequest
    {
        [JsonPropertyName("intended_payment_methods")]
        public List<PaymentMethod>? IntendedPaymentMethods { get; set; }

        [JsonPropertyName("intended_services")]
        public List<IntendedService>? IntendedServices { get; set; }
    }


    public class PaymentRequirements
    {
        [JsonPropertyName("requires_instant_payment")]
        public bool RequiresInstantPayment { get; set; }
        [JsonPropertyName("price_guarantee_expires_at")]
        public DateTime? PriceGuaranteeExpiresAt { get; set; }
        [JsonPropertyName("payment_required_by")]
        public DateTime? PaymentRequiredBy { get; set; }
    }

    public class Slice
    {
        [JsonPropertyName("comparison_key")]
        public string? ComparisonKey { get; set; }
        [JsonPropertyName("ngs_shelf")]
        public int? NgsShelf { get; set; }
        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; set; }
        [JsonPropertyName("origin_type")]
        public string? OriginType { get; set; }
        [JsonPropertyName("fare_brand_name")]
        public string? FareBrandName { get; set; }
        [JsonPropertyName("segments")]
        public List<Segment>? Segments { get; set; }
        [JsonPropertyName("conditions")]
        public SliceConditions? Conditions { get; set; }
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
        [JsonPropertyName("destination")]
        public Location? Destination { get; set; }
        [JsonPropertyName("origin")]
        public Location? Origin { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class Segment
    {
        [JsonPropertyName("origin_terminal")]
        public string? OriginTerminal { get; set; }
        [JsonPropertyName("destination_terminal")]
        public string? DestinationTerminal { get; set; }
        [JsonPropertyName("aircraft")]
        public object? Aircraft { get; set; }
        [JsonPropertyName("departing_at")]
        public DateTime DepartingAt { get; set; }
        [JsonPropertyName("arriving_at")]
        public DateTime ArrivingAt { get; set; }
        [JsonPropertyName("stops")]
        public List<object>? Stops { get; set; }
        [JsonPropertyName("operating_carrier")]
        public Carrier? OperatingCarrier { get; set; }
        [JsonPropertyName("marketing_carrier")]
        public Carrier? MarketingCarrier { get; set; }
        [JsonPropertyName("operating_carrier_flight_number")]
        public string? OperatingCarrierFlightNumber { get; set; }
        [JsonPropertyName("marketing_carrier_flight_number")]
        public string? MarketingCarrierFlightNumber { get; set; }
        [JsonPropertyName("passengers")]
        public List<PassengerSegmentInfo>? Passengers { get; set; }
        [JsonPropertyName("distance")]
        public string? Distance { get; set; }
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
        [JsonPropertyName("destination")]
        public Location? Destination { get; set; }
        [JsonPropertyName("origin")]
        public Location? Origin { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class PassengerSegmentInfo
    {
        [JsonPropertyName("baggages")]
        public List<Baggage>? Baggages { get; set; }
        [JsonPropertyName("cabin_class_marketing_name")]
        public string? CabinClassMarketingName { get; set; }
        [JsonPropertyName("passenger_id")]
        public string? PassengerId { get; set; }
        [JsonPropertyName("cabin")]
        public Cabin? Cabin { get; set; }
        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; set; }
        [JsonPropertyName("fare_basis_code")]
        public string? FareBasisCode { get; set; }
    }

    public class Baggage
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Cabin
    {
        [JsonPropertyName("amenities")]
        public CabinAmenities? Amenities { get; set; }
        [JsonPropertyName("marketing_name")]
        public string? MarketingName { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class CabinAmenities
    {
        [JsonPropertyName("seat")]
        public Seat? Seat { get; set; }
        [JsonPropertyName("wifi")]
        public Wifi? Wifi { get; set; }
        [JsonPropertyName("power")]
        public Power? Power { get; set; }
    }

    public class Seat
    {
        [JsonPropertyName("pitch")]
        public string? Pitch { get; set; }
        [JsonPropertyName("legroom")]
        public string? Legroom { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Wifi
    {
        [JsonPropertyName("cost")]
        public string? Cost { get; set; }
        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }

    public class Power
    {
        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }

    public class Carrier
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

    public class DuffelConditions
    {
        [JsonPropertyName("refund_before_departure")]
        public ChangeCondition? RefundBeforeDeparture { get; set; }
        [JsonPropertyName("change_before_departure")]
        public ChangeCondition? ChangeBeforeDeparture { get; set; }
    }

    public class SliceConditions
    {
        [JsonPropertyName("change_before_departure")]
        public ChangeCondition? ChangeBeforeDeparture { get; set; }
        [JsonPropertyName("priority_check_in")]
        public object? PriorityCheckIn { get; set; }
        [JsonPropertyName("priority_boarding")]
        public object? PriorityBoarding { get; set; }
        [JsonPropertyName("advance_seat_selection")]
        public object? AdvanceSeatSelection { get; set; }
    }

    public class ChangeCondition
    {
        [JsonPropertyName("penalty_currency")]
        public string? PenaltyCurrency { get; set; }
        [JsonPropertyName("penalty_amount")]
        public string? PenaltyAmount { get; set; }
        [JsonPropertyName("allowed")]
        public bool Allowed { get; set; }
    }



    public class DuffelOfferService
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("maximum_quantity")]
        public int? MaximumQuantity { get; set; }
        [JsonPropertyName("segment_ids")]
        public List<string>? SegmentIds { get; set; }
        [JsonPropertyName("passenger_ids")]
        public List<string>? PassengerIds { get; set; }
        [JsonPropertyName("total_currency")]
        public string? TotalCurrency { get; set; }
        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }
        [JsonPropertyName("metadata")]
        public ServiceMetadata? Metadata { get; set; }
    }

    public class ServiceMetadata
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("maximum_weight_kg")]
        public int? MaximumWeightKg { get; set; }
        [JsonPropertyName("maximum_length_cm")]
        public int? MaximumLengthCm { get; set; }
        [JsonPropertyName("maximum_height_cm")]
        public int? MaximumHeightCm { get; set; }
        [JsonPropertyName("maximum_depth_cm")]
        public int? MaximumDepthCm { get; set; }
    }

    public class IntendedService
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class PaymentMethod
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
        [JsonPropertyName("amount")]
        public string? Amount { get; set; }
    }


}
