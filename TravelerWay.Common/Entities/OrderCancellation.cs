using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class OrderCancellation
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string DuffelCancellationId { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal RefundAmount { get; set; }
    public string RefundCurrency { get; set; } = "USD";

    public string Status { get; set; } = "Requested"; // Requested | Confirmed | Failed

    public string? StripeRefundId { get; set; }
    public string RawCancellationJson { get; set; } = "{}";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ConfirmedAt { get; set; }
}
