using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;

    public Offer? Offer { get; set; } = null!;
    public string? BookingOrderId { get; set; }
    public string? BookingReference { get; set; }

    public OrderStatus? Status { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    public string TotalCurrency { get; set; } = "USD";

    public DateTime? DocumentsIssuedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Ancillary> Ancillaries { get; set; } = new List<Ancillary>();
    public ICollection<OrderChange> OrderChanges { get; set; } = new List<OrderChange>();
    public ICollection<OrderCancellation> OrderCancellations { get; set; } = new List<OrderCancellation>();
    public ICollection<AirlineCredit> AirlineCredits { get; set; } = new List<AirlineCredit>();
}

public enum OrderStatus
{
    Pending,
    Booked,
    Cancelled,
    Failed
}
