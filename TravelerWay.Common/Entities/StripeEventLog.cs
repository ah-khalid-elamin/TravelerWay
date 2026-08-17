using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerWay.Common.Entities
{
    public class StripeEventLog
    {
        public Guid Id { get; set; }
        public string? StripeEventId { get; set; }
        public string? EventName { get; set; }
        public string? OfferId { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}
