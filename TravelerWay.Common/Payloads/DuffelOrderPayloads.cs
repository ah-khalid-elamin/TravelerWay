using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{
    public class DuffelOrderResponse
    {
        [JsonPropertyName("void_window_ends_at")]
        public DateTime? VoidWindowEndsAt { get; set; }

        [JsonPropertyName("synced_at")]
        public DateTime? SyncedAt { get; set; }

        [JsonPropertyName("available_actions")]
        public List<string> AvailableActions { get; set; } = new();

        [JsonPropertyName("airline_initiated_changes")]
        public List<object> AirlineInitiatedChanges { get; set; } = new();

        [JsonPropertyName("tax_currency")]
        public string? TaxCurrency { get; set; }

        [JsonPropertyName("documents")]
        public List<DuffelDocument> Documents { get; set; } = new();

        [JsonPropertyName("tax_amount")]
        public string? TaxAmount { get; set; }

        [JsonPropertyName("base_currency")]
        public string? BaseCurrency { get; set; }

        [JsonPropertyName("base_amount")]
        public string? BaseAmount { get; set; }

        [JsonPropertyName("total_currency")]
        public string? TotalCurrency { get; set; }

        [JsonPropertyName("offer_id")]
        public string? OfferId { get; set; }

        [JsonPropertyName("booking_reference")]
        public string? BookingReference { get; set; }

        [JsonPropertyName("payment_status")]
        public DuffelPaymentStatus? PaymentStatus { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("live_mode")]
        public bool LiveMode { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("slices")]
        public List<Slice> Slices { get; set; } = new();

        [JsonPropertyName("passengers")]
        public List<Passenger> Passengers { get; set; } = new();

        [JsonPropertyName("cancellation")]
        public object? Cancellation { get; set; }

        [JsonPropertyName("conditions")]
        public DuffelConditions? Conditions { get; set; }

        [JsonPropertyName("cancelled_at")]
        public DateTime? CancelledAt { get; set; }

        [JsonPropertyName("changes")]
        public List<object> Changes { get; set; } = new();

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("services")]
        public List<DuffelOrderService> Services { get; set; } = new();

        [JsonPropertyName("users")]
        public List<object> Users { get; set; } = new();

        [JsonPropertyName("metadata")]
        public Dictionary<string, object>? Metadata { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("owner")]
        public DuffelCarrier? Owner { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }


    public class DuffelDocument
    {
        [JsonPropertyName("passenger_ids")]
        public List<string> PassengerIds { get; set; } = new();

        [JsonPropertyName("unique_identifier")]
        public string? UniqueIdentifier { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class DuffelPaymentStatus
    {
        [JsonPropertyName("price_guarantee_expires_at")]
        public DateTime? PriceGuaranteeExpiresAt { get; set; }

        [JsonPropertyName("payment_required_by")]
        public DateTime? PaymentRequiredBy { get; set; }

        [JsonPropertyName("paid_at")]
        public DateTime? PaidAt { get; set; }

        [JsonPropertyName("awaiting_payment")]
        public bool AwaitingPayment { get; set; }
    }

    public class DuffelCarrier
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

    public class DuffelPenalty
    {
        [JsonPropertyName("penalty_currency")]
        public string? PenaltyCurrency { get; set; }

        [JsonPropertyName("penalty_amount")]
        public string? PenaltyAmount { get; set; }

        [JsonPropertyName("allowed")]
        public bool Allowed { get; set; }
    }


    public class DuffelServiceMetadata
    {
        [JsonPropertyName("maximum_length_cm")]
        public double? MaximumLengthCm { get; set; }

        [JsonPropertyName("maximum_height_cm")]
        public double? MaximumHeightCm { get; set; }

        [JsonPropertyName("maximum_depth_cm")]
        public double? MaximumDepthCm { get; set; }

        [JsonPropertyName("maximum_weight_kg")]
        public double? MaximumWeightKg { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class DuffelOrderRequest
    {
        [JsonPropertyName("users")]
        public List<string> Users { get; set; } = new();

        [JsonPropertyName("type")]
        public string? Type { get; set; } // hold vs instant

        [JsonPropertyName("services")]
        public List<DuffelOrderRequestService> Services { get; set; } = new();

        [JsonPropertyName("selected_offers")]
        public List<string> SelectedOffers { get; set; } = new();

        [JsonPropertyName("payments")]
        public List<DuffelPayment> Payments { get; set; } = new();

        [JsonPropertyName("passengers")]
        public List<DuffelOfferPassenger> Passengers { get; set; } = new();

        [JsonPropertyName("metadata")]
        public Dictionary<string, object>? Metadata { get; set; }
    }



    public class DuffelOrderRequestService
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class DuffelPayment
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("three_d_secure_session_id")]
        public string? ThreeDSecureSessionId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }
    }

    public class DuffelOrderPassenger
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
        public DateOnly? BornOn { get; set; }
    }
    public class DuffelOrderService
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

    public class DuffelOrderRequestIdentityDocument
    {
        [JsonPropertyName("unique_identifier")]
        public string? UniqueIdentifier { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("issuing_country_code")]
        public string? IssuingCountryCode { get; set; }

        [JsonPropertyName("expires_on")]
        public DateOnly? ExpiresOn { get; set; }
    }

    public class DuffelOrderServiceAdditionRequest
    {
        [JsonPropertyName("payment")]
        public DuffelPayment Payment { get; set; } = new();

        [JsonPropertyName("add_services")]
        public List<DuffelOrderRequestService> AddServices { get; set; } = new();
    }

    public class DuffelUpdateOrderRequest
    {
        [JsonPropertyName("users")]
        public List<string> Users { get; set; } = new();

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; } = new();
    }



}
