using System.Text.Json.Serialization;

namespace TravelerWay.Common.Payloads;


//public class DuffelListCustomersResponse
//{
//    [JsonPropertyName("data")]
//    public List<DuffelCustomerResponse> Data { get; set; } = new List<DuffelCustomerResponse>();
//    [JsonPropertyName("meta")]
//    public DuffelMetaData Meta { get; set; } = new DuffelMetaData();
//}

public class DuffelCustomerRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; } = string.Empty;

    [JsonPropertyName("given_name")]
    public string GivenName { get; set; } = string.Empty;

    [JsonPropertyName("group_id")]
    public string? GroupId { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("preferred_language")]
    public string? PreferredLanguage { get; set; }
}
public class DuffelCustomerResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("given_name")]
    public string? GivenName { get; set; }

    [JsonPropertyName("family_name")]
    public string? FamilyName { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("preferred_language")]
    public string? PreferredLanguage { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("live_mode")]
    public bool? LiveMode { get; set; }

    [JsonPropertyName("group")]
    public DuffelCustomerGroup? Group { get; set; }
}

public class DuffelCustomerGroup
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
