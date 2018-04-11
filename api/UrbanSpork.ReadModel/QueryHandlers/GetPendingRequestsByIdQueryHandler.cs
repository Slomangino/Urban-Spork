using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetPendingRequestsByIdQueryHandler : IQueryHandler<GetPendingRequestsByIdQuery, List<PendingRequestsProjection>>
    {
        private UrbanDbContext _context;

        public GetPendingRequestsByIdQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }
        public async Task<List<PendingRequestsProjection>> Handle(GetPendingRequestsByIdQuery query)
        {
            return await _context.PendingRequestsProjection.Where(a => a.ForId == query.Id).ToListAsync();
        }
    }
}