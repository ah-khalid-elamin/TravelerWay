using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class OrderChange
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string OrderChangeRequestId { get; set; } = string.Empty;
    public string? OrderChangeOfferId { get; set; }
    public string? OrderChangeId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ChangeTotalAmount { get; set; }
    public string ChangeCurrency { get; set; } = "USD";

    public string Status { get; set; } = "Requested"; // Requested | OfferSelected | AwaitingPayment | Confirmed | Failed

    public string? StripePaymentIntentId { get; set; }
    public string RefundStatus { get; set; } = "NotApplicable"; // NotApplicable | Pending | Refunded

    public string RawChangeJson { get; set; } = "{}";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
