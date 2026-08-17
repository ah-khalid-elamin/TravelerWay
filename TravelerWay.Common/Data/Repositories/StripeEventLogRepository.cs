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
    public class StripeEventLogRepository : Repository<StripeEventLog>, IStripeEventLogRepository
    {
        public StripeEventLogRepository(TravelerWayDbContext context) : base(context)
        {
        }

        public async Task<StripeEventLog?> GetEventLogByEventIdAndNameAsync(string eventId, string eventName)
        {
            var list = await GetAllAsync();
            return list.FirstOrDefault(e => e.StripeEventId == eventId && e.EventName == eventName);
        }
    }
}
