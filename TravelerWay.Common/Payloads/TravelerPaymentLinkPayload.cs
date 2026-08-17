using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelerWay.Common.Payloads
{
    public class PaymentLinkRequest
    {
        [JsonPropertyName("offer_id")]
        public string OfferId { get; set; } = string.Empty;
        [JsonPropertyName("offer_request_id")]
        public string OfferRequestId { get; set; } = string.Empty;
        [JsonPropertyName("passengers")]
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
