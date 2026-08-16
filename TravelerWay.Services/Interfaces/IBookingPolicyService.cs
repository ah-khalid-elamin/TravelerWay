using TravelerWay.Common.Models;

namespace TravelerWay.Services.Interfaces;

public interface IBookingPolicyService
{
    PricingResult CalculatePricing(decimal baseFare, BookingTier tier, IEnumerable<LuggageSelection> luggage);
    CancellationDecision EvaluateCancellation(decimal totalAmount, BookingTier tier, DateTime bookingTime, DateTime cancellationTime);
    RescheduleDecision EvaluateReschedule(BookingTier tier, DateTime requestedDepartureTime, DateTime currentDepartureTime);
}
