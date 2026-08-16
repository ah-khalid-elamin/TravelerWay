using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class OrderChangeOffer
{
    [Key]
    public string Id { get; set; } = string.Empty; // Duffel ID: oco_xxx

    [Required]
    public string OrderChangeRequestId { get; set; } = string.Empty;
    public OrderChangeRequest OrderChangeRequest { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal ChangeTotalAmount { get; set; } // Negative indicates refund due
    public string ChangeTotalCurrency { get; set; } = "USD";

    [Column(TypeName = "decimal(18,2)")]
    public decimal NewTotalAmount { get; set; }
    public string NewTotalCurrency { get; set; } = "USD";

    [Column(TypeName = "decimal(18,2)")]
    public decimal PenaltyTotalAmount { get; set; }
    public string PenaltyTotalCurrency { get; set; } = "USD";

    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public OrderChange? OrderChange { get; set; }
}
