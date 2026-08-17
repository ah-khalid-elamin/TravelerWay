using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelerWay.Common.Entities;

public class Search
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? BookingOfferRequestId { get; set; }

    public string CabinClass { get; set; } = "economy"; // Economy | PremiumEconomy | Business | First

    public int? MaxConnections { get; set; }
    public string? Sort { get; set; } = "total_amount"; // Cheapest | Fastest | FewestConnections
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
