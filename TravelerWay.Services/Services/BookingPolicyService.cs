using TravelerWay.Common.Models;
using TravelerWay.Services.Interfaces;

namespace TravelerWay.Services.Services;

public class BookingPolicyService : IBookingPolicyService
{
    private const decimal BaseTierFee = 20m;
    private const decimal FlexTierFee = 35m;
    private const decimal ApexTierFee = 60m;
    private const decimal CheckedBagFee = 35m;
    private const decimal SpecialEquipmentFee = 75m;

    public PricingResult CalculatePricing(decimal baseFare, BookingTier tier, IEnumerable<LuggageSelection> luggage)
    {
        var tierFee = tier switch
        {
            BookingTier.Base => BaseTierFee,
            BookingTier.Flex => FlexTierFee,
            BookingTier.Apex => ApexTierFee,
            _ => 0m
        };

        var luggageFee = luggage.Sum(selection => selection.Type switch
        {
            LuggageType.CheckedBag => selection.Quantity * CheckedBagFee,
            LuggageType.SpecialEquipment => selection.Quantity * SpecialEquipmentFee,
            _ => 0m
        });

        return new PricingResult
        {
            BaseFare = baseFare,
            TierFee = tierFee,
            LuggageFee = luggageFee,
            TotalAmount = baseFare + tierFee + luggageFee
        };
    }

    public CancellationDecision EvaluateCancellation(decimal totalAmount, BookingTier tier, DateTime bookingTime, DateTime cancellationTime)
    {
        var hoursUntilDeparture = (bookingTime - cancellationTime).TotalHours;
        var isWithin48Hours = hoursUntilDeparture <= 48;
        var isWithin24Hours = hoursUntilDeparture <= 24;

        if (tier == BookingTier.Apex && isWithin48Hours)
        {
            return new CancellationDecision
            {
                Outcome = RefundOutcome.FullRefund,
                RefundAmount = totalAmount,
                Message = "Apex-tier bookings receive a full refund within 48 hours."
            };
        }

        if (isWithin24Hours)
        {
            return new CancellationDecision
            {
                Outcome = RefundOutcome.AgencyCredit,
                RefundAmount = totalAmount * 0.5m,
                Message = "A 50% agency credit is issued for cancellations within 24 hours."
            };
        }

        return new CancellationDecision
        {
            Outcome = RefundOutcome.NoRefund,
            RefundAmount = 0m,
            Message = "No refund is available outside the protected window."
        };
    }

    public RescheduleDecision EvaluateReschedule(BookingTier tier, DateTime requestedDepartureTime, DateTime currentDepartureTime)
    {
        var hoursBeforeDeparture = (requestedDepartureTime - currentDepartureTime).TotalHours;
        if (hoursBeforeDeparture <= 4)
        {
            return new RescheduleDecision
            {
                IsAllowed = false,
                AdditionalCost = 0m,
                Reason = "Rescheduling is not permitted within four hours of departure."
            };
        }

        var penalty = tier switch
        {
            BookingTier.Base => 40m,
            BookingTier.Flex => 20m,
            BookingTier.Apex => 0m,
            _ => 0m
        };

        return new RescheduleDecision
        {
            IsAllowed = true,
            AdditionalCost = penalty,
            Reason = "Reschedule approved."
        };
    }
}
