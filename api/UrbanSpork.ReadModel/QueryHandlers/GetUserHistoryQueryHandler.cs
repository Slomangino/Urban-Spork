using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetUserHistoryQueryHandler : IQueryHandler<GetUserHistoryQuery, List<UserHistoryProjection>>
    {
        private readonly UrbanDbContext _context;

        public GetUserHistoryQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserHistoryProjection>> Handle(GetUserHistoryQuery query)
        {
            var filteredList = await Filter(query.FilterCriteria).ToListAsync();
            return filteredList;
        }

        private IQueryable<UserHistoryProjection> Filter(UserHistoryFilterCriteria criteria)
        {
            IQueryable<UserHistoryProjection> query = _context.UserHistoryProjection;

            if (criteria.UserId != Guid.Empty)
            {
                query = query.Where(a => a.UserId == criteria.UserId);
            }

            query = query.OrderByDescending(a => a.TimeStamp);

            return query;
        }
    }
}
