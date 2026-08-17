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
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(TravelerWayDbContext context) : base(context)
        {
        }

        public async Task<List<Passenger>> GetPassengersByBookingOfferIdAsync(string bookingOfferId)
        {
            var list = await GetAllAsync();
            return  list.Where(p => p.BookingOfferId == bookingOfferId).ToList();
        }
    }
}
