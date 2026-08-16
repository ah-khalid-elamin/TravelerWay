using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerWay.Api.Data;
using TravelerWay.Common.Entities;

namespace TravelerWay.Common.Data.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(TravelerWayDbContext context) : base(context)
        {
        }
    }
}
