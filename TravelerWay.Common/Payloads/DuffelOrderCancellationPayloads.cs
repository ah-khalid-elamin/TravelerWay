using System;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{
    public class DuffelOrderCancellationResponse
    {
        [JsonPropertyName("refund_to")]
        public string RefundTo { get; set; } = string.Empty;

        [JsonPropertyName("refund_currency")]
        public string RefundCurrency { get; set; } = string.Empty;

        // The API represents amounts as strings (e.g. "90.80")
        [JsonPropertyName("refund_amount")]
        public string RefundAmount { get; set; } = string.Empty;

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("live_mode")]
        public bool LiveMode { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("expires_at")]
        public DateTimeOffset? ExpiresAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("confirmed_at")]
        public DateTimeOffset? ConfirmedAt { get; set; }
    }

    public class DuffelOrderCancellationRequest
    {
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }
}
