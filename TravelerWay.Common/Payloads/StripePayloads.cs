using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{

    public class CheckoutSessionRequest
    {
        public string Mode { get; set; } = "payment";
        public string IdempotencyKey { get; set; } = string.Empty;
        public string SuccessUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;
        public List<LineItem> LineItems { get; set; } = new();
        public Dictionary<string, string> Metadata { get; set; } = new();
    }

    public class LineItem
    {
        public int? Quantity { get; set; }
        public PriceData PriceData { get; set; } = new();
    }

    public class PriceData
    {
        public string? Currency { get; set; }
        public int? UnitAmount { get; set; }
        public ProductData ProductData { get; set; } = new();
    }

    public class ProductData
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string> Images { get; set; } = new();
    }
    public class CheckoutSessionResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("object")]
        public string? Object { get; init; }

        [JsonPropertyName("adaptive_pricing")]
        public AdaptivePricing? AdaptivePricing { get; init; }

        [JsonPropertyName("after_expiration")]
        public object? AfterExpiration { get; init; }

        [JsonPropertyName("allow_promotion_codes")]
        public bool? AllowPromotionCodes { get; init; }

        [JsonPropertyName("amount_subtotal")]
        public long? AmountSubtotal { get; init; }

        [JsonPropertyName("amount_total")]
        public long? AmountTotal { get; init; }

        [JsonPropertyName("automatic_tax")]
        public AutomaticTax? AutomaticTax { get; init; }

        [JsonPropertyName("billing_address_collection")]
        public string? BillingAddressCollection { get; init; }

        [JsonPropertyName("branding_settings")]
        public BrandingSettings? BrandingSettings { get; init; }

        [JsonPropertyName("cancel_url")]
        public string? CancelUrl { get; init; }

        [JsonPropertyName("client_reference_id")]
        public string? ClientReferenceId { get; init; }

        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; init; }

        [JsonPropertyName("collected_information")]
        public object? CollectedInformation { get; init; }

        [JsonPropertyName("consent")]
        public object? Consent { get; init; }

        [JsonPropertyName("consent_collection")]
        public object? ConsentCollection { get; init; }

        [JsonPropertyName("created")]
        public long? Created { get; init; }

        [JsonPropertyName("currency")]
        public string? Currency { get; init; }

        [JsonPropertyName("currency_conversion")]
        public object? CurrencyConversion { get; init; }

        [JsonPropertyName("custom_fields")]
        public List<object>? CustomFields { get; init; }

        [JsonPropertyName("custom_text")]
        public CustomText? CustomText { get; init; }

        [JsonPropertyName("customer")]
        public string? Customer { get; init; }

        [JsonPropertyName("customer_account")]
        public string? CustomerAccount { get; init; }

        [JsonPropertyName("customer_creation")]
        public string? CustomerCreation { get; init; }

        [JsonPropertyName("customer_details")]
        public object? CustomerDetails { get; init; }

        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; init; }

        [JsonPropertyName("discounts")]
        public List<object>? Discounts { get; init; }

        [JsonPropertyName("expires_at")]
        public long? ExpiresAt { get; init; }

        [JsonPropertyName("integration_identifier")]
        public string? IntegrationIdentifier { get; init; }

        [JsonPropertyName("invoice")]
        public string? Invoice { get; init; }

        [JsonPropertyName("invoice_creation")]
        public InvoiceCreation? InvoiceCreation { get; init; }

        [JsonPropertyName("livemode")]
        public bool? Livemode { get; init; }

        [JsonPropertyName("locale")]
        public string? Locale { get; init; }

        [JsonPropertyName("managed_payments")]
        public ManagedPayments? ManagedPayments { get; init; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string>? Metadata { get; init; }

        [JsonPropertyName("mode")]
        public string? Mode { get; init; }

        [JsonPropertyName("origin_context")]
        public object? OriginContext { get; init; }

        [JsonPropertyName("payment_intent")]
        public string? PaymentIntent { get; init; }

        [JsonPropertyName("payment_link")]
        public string? PaymentLink { get; init; }

        [JsonPropertyName("payment_method_collection")]
        public string? PaymentMethodCollection { get; init; }

        [JsonPropertyName("payment_method_configuration_details")]
        public PaymentMethodConfigurationDetails? PaymentMethodConfigurationDetails { get; init; }

        [JsonPropertyName("payment_method_options")]
        public PaymentMethodOptions? PaymentMethodOptions { get; init; }

        [JsonPropertyName("payment_method_types")]
        public List<string>? PaymentMethodTypes { get; init; }

        [JsonPropertyName("payment_status")]
        public string? PaymentStatus { get; init; }

        [JsonPropertyName("permissions")]
        public object? Permissions { get; init; }

        [JsonPropertyName("phone_number_collection")]
        public PhoneNumberCollection? PhoneNumberCollection { get; init; }

        [JsonPropertyName("recovered_from")]
        public object? RecoveredFrom { get; init; }

        [JsonPropertyName("saved_payment_method_options")]
        public object? SavedPaymentMethodOptions { get; init; }

        [JsonPropertyName("setup_intent")]
        public string? SetupIntent { get; init; }

        [JsonPropertyName("shipping_address_collection")]
        public object? ShippingAddressCollection { get; init; }

        [JsonPropertyName("shipping_cost")]
        public object? ShippingCost { get; init; }

        [JsonPropertyName("shipping_options")]
        public List<object>? ShippingOptions { get; init; }

        [JsonPropertyName("status")]
        public string? Status { get; init; }

        [JsonPropertyName("submit_type")]
        public string? SubmitType { get; init; }

        [JsonPropertyName("subscription")]
        public string? Subscription { get; init; }

        [JsonPropertyName("success_url")]
        public string? SuccessUrl { get; init; }

        [JsonPropertyName("total_details")]
        public TotalDetails? TotalDetails { get; init; }

        [JsonPropertyName("ui_mode")]
        public string? UiMode { get; init; }

        [JsonPropertyName("url")]
        public string? Url { get; init; }

        [JsonPropertyName("wallet_options")]
        public object? WalletOptions { get; init; }
    }

    public class AdaptivePricing
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; init; }
    }

    public class AutomaticTax
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; init; }

        [JsonPropertyName("liability")]
        public string? Liability { get; init; }

        [JsonPropertyName("provider")]
        public object? Provider { get; init; }

        [JsonPropertyName("status")]
        public string? Status { get; init; }
    }

    public class BrandingSettings
    {
        [JsonPropertyName("background_color")]
        public string? BackgroundColor { get; init; }

        [JsonPropertyName("border_style")]
        public string? BorderStyle { get; init; }

        [JsonPropertyName("button_color")]
        public string? ButtonColor { get; init; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; init; }

        [JsonPropertyName("font_family")]
        public string? FontFamily { get; init; }

        [JsonPropertyName("icon")]
        public FileReference? Icon { get; init; }

        [JsonPropertyName("logo")]
        public FileReference? Logo { get; init; }
    }

    public class FileReference
    {
        [JsonPropertyName("file")]
        public string? File { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }
    }

    public class InvoiceCreation
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; init; }

        [JsonPropertyName("invoice_data")]
        public InvoiceData? InvoiceData { get; init; }
    }

    public class InvoiceData
    {
        [JsonPropertyName("account_tax_ids")]
        public object? AccountTaxIds { get; init; }

        [JsonPropertyName("custom_fields")]
        public object? CustomFields { get; init; }

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("footer")]
        public string? Footer { get; init; }

        [JsonPropertyName("issuer")]
        public object? Issuer { get; init; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, object>? Metadata { get; init; }

        [JsonPropertyName("rendering_options")]
        public object? RenderingOptions { get; init; }
    }

    public class ManagedPayments
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; init; }
    }

    public class PaymentMethodConfigurationDetails
    {
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("parent")]
        public object? Parent { get; init; }
    }

    public class PaymentMethodOptions
    {
        [JsonPropertyName("card")]
        public CardOptions? Card { get; init; }
    }

    public class CardOptions
    {
        [JsonPropertyName("request_three_d_secure")]
        public string? RequestThreeDSecure { get; init; }
    }

    public class PhoneNumberCollection
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; init; }
    }

    public class CustomText
    {
        [JsonPropertyName("after_submit")]
        public object? AfterSubmit { get; init; }

        [JsonPropertyName("shipping_address")]
        public object? ShippingAddress { get; init; }

        [JsonPropertyName("submit")]
        public object? Submit { get; init; }

        [JsonPropertyName("terms_of_service_acceptance")]
        public object? TermsOfServiceAcceptance { get; init; }
    }

    public class TotalDetails
    {
        [JsonPropertyName("amount_discount")]
        public long? AmountDiscount { get; init; }

        [JsonPropertyName("amount_shipping")]
        public long? AmountShipping { get; init; }

        [JsonPropertyName("amount_tax")]
        public long? AmountTax { get; init; }
    }
}
