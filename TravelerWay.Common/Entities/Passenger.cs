using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelerWay.Common.Entities;

public class Passenger
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SearchId { get; set; }
    public Search Search { get; set; } = null!;

    public string? PassengerId { get; set; }
    public string PassengerType { get; set; } = "adult";

    public string? Title { get; set; }
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? Gender { get; set; }
    public string? BornOn { get; set; }
    public string? BookingOfferId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public string? DocumentExpiryDate { get; set; }
    public string? DocumentIssuingCountry { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
