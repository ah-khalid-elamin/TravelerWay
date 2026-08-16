using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class Ancillary
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid OfferId { get; set; }
    public Offer? Offer { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }

    public string DuffelServiceId { get; set; } = string.Empty;
    public string ServiceType { get; set; } = "Other"; // Baggage | Seat | Meal | Other

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitAmount { get; set; }
    public string Currency { get; set; } = "USD";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
