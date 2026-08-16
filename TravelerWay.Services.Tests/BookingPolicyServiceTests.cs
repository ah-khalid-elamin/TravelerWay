using TravelerWay.Common.Models;
using TravelerWay.Services.Interfaces;
using TravelerWay.Services.Services;

namespace TravelerWay.Services.Tests;

public class BookingPolicyServiceTests
{
    [Fact]
    public void CalculatePricing_AddsTierFeeAndLuggageCharges()
    {
        var service = new BookingPolicyService();

        var result = service.CalculatePricing(250m, BookingTier.Flex, new[]
        {
            new LuggageSelection { Type = LuggageType.CheckedBag, Quantity = 2 },
            new LuggageSelection { Type = LuggageType.SpecialEquipment, Quantity = 1 }
        });

        Assert.Equal(250m + 35m + 75m, result.TotalAmount);
        Assert.Equal(35m, result.TierFee);
        Assert.Equal(75m, result.LuggageFee);
    }

    [Fact]
    public void EvaluateCancellation_ReturnsFullRefundForWithin48HoursForApex()
    {
        var service = new BookingPolicyService();

        var result = service.EvaluateCancellation(300m, BookingTier.Apex, DateTime.UtcNow.AddHours(12), DateTime.UtcNow.AddDays(1));

        Assert.Equal(300m, result.RefundAmount);
        Assert.Equal(RefundOutcome.FullRefund, result.Outcome);
    }

    [Fact]
    public void EvaluateReschedule_RejectsRequestsWithinFourHoursOfDeparture()
    {
        var service = new BookingPolicyService();

        var result = service.EvaluateReschedule(BookingTier.Base, DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(3));

        Assert.False(result.IsAllowed);
        Assert.Equal("Rescheduling is not permitted within four hours of departure.", result.Reason);
    }
}
