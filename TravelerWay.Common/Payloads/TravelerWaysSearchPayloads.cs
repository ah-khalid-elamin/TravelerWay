using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelerWay.Common.Payloads
{
    public class TravelerWaysSearchResponse
    {
        [JsonPropertyName("meta")]
        public DuffelPaginationFilters? Meta { get; set; }

        [JsonPropertyName("offer_request_id")]
        public string? OfferRequestId { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<DuffelOfferResponse>? Data { get; set; }
    }
}
