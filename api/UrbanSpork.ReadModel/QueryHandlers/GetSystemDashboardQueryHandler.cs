using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetSystemDashboardQueryHandler : IQueryHandler<GetSystemDashboardQuery, List<DashboardProjection>>
    {
        private readonly UrbanDbContext _context;

        public GetSystemDashboardQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<DashboardProjection>> Handle(GetSystemDashboardQuery query)
        {
            IQueryable<DashboardProjection> queryable = _context.DashBoardProjection;

            //ordering by most pending requests and then by most active users
            queryable = queryable.OrderByDescending(a => a.PendingRequests)
                .ThenByDescending(a => a.ActiveUsers);

            var list = await queryable.ToListAsync();

            //return whole list if it is less than 15 entries
            if (list.Count < 15)
            {
                return list;
            }

            //otherwise return the truncated list of top 15 results
            return list.GetRange(0, 14);
        }
    }
}
