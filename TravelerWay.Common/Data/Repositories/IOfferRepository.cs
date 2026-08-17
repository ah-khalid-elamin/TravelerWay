using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerWay.Common.Entities;

namespace TravelerWay.Common.Data.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        public Task<Offer> GetOfferByBookingOfferIdAsync(string bookingOfferId);
    }
}
