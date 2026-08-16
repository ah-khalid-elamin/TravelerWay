using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace TravelerWay.Common.Payloads
{

    public record DuffelRequest<T>(
        [property: JsonPropertyName("data")] T Data
    );

    public record DuffelResponse<T>(
        [property: JsonPropertyName("data")] T Data
    );

    public record DuffelResponseWithMetaData<M, D>(
        [property: JsonPropertyName("meta")] M Meta,
        [property: JsonPropertyName("data")] D Data

    );


    public record DuffelPaginationFilters
    {
        public int? Limit { get; set; } = 50;
        public string? Before { get; set; } = string.Empty;
        public string? After { get; set; } = string.Empty;
    }

}
