using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class AirlineCredit
{
    [Key]
    public string Id { get; set; } = string.Empty; // Duffel ID: acd_xxx

    public string Code { get; set; } = string.Empty; // Ticket number or voucher code
    public string? Type { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    public string AmountCurrency { get; set; } = "USD";

    public string OwnerIataCode { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;

    public DateOnly IssuedOn { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? SpentAt { get; set; }
    public DateTime? InvalidatedAt { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
