using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelerWay.Common.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public ChannelType? ChannelType { get; set; } 
    public string ChatId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? PreferredLanguage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Search> Searches { get; set; } = new List<Search>();
    public ICollection<AirlineCredit> AirlineCredits { get; set; } = new List<AirlineCredit>();
}

public enum ChannelType
{
    WhatsApp,
    Telegram,
    Web,
    None
}
