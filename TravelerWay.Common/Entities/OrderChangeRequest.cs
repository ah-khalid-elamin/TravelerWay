using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelerWay.Common.Entities;

public class OrderChangeRequest
{
    [Key]
    public string Id { get; set; } = string.Empty; // Duffel ID: ocr_xxx

    [Required]
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string SlicesToAddJson { get; set; } = "[]";
    public string SlicesToRemoveJson { get; set; } = "[]";

    public bool LiveMode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<OrderChangeOffer> OrderChangeOffers { get; set; } = new List<OrderChangeOffer>();
}
