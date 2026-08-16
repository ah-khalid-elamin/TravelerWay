using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads
{
    public class DuffelPassengerRequest
    {
        [JsonPropertyName("loyalty_programme_accounts")]
        public List<LoyaltyProgrammeAccount>? LoyaltyProgrammeAccounts { get; set; }

        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }

        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }
    }

    public class DuffelPassengerResponse
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("loyalty_programme_accounts")]
        public List<LoyaltyProgrammeAccount>? LoyaltyProgrammeAccounts { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }

        [JsonPropertyName("fare_type")]
        public string? FareType { get; set; }

        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }
    }

    public class LoyaltyProgrammeAccount
    {
        [JsonPropertyName("airline_iata_code")]
        public string? AirlineIataCode { get; set; }

        [JsonPropertyName("account_number")]
        public string? AccountNumber { get; set; }
    }
}
