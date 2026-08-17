using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerWay.Api.Data;
using TravelerWay.Common.Entities;

namespace TravelerWay.Common.Data.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(TravelerWayDbContext context) : base(context)
        {


        }
        public async Task<Offer> GetOfferByBookingOfferIdAsync(string? bookingOfferId)
        {
            if (string.IsNullOrWhiteSpace(bookingOfferId)) throw new ArgumentException("The booking offer id can't be null");
            var offers = await GetAllAsync();

            return offers.ToList().Find(x => x.BookingOfferId == bookingOfferId)!;
        }
    }
}