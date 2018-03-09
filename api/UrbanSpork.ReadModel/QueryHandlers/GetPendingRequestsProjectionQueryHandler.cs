using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetPendingRequestsProjectionQueryHandler : IQueryHandler<GetPendingRequestsProjectionQuery, List<PendingRequestsProjection>>
    {
        private UrbanDbContext _context;

        public GetPendingRequestsProjectionQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public Task<List<PendingRequestsProjection>> Handle(GetPendingRequestsProjectionQuery query)
        {
          return  _context.PendingRequestsProjection.ToListAsync();
        }
    }
}
