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
    public class SearchRepository : Repository<Search>, ISearchRepository
    {
        public SearchRepository(TravelerWayDbContext context) : base(context)
        {
        }
    }
}
