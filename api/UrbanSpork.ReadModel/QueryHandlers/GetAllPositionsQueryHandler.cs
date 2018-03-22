using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetAllPositionsQueryHandler : IQueryHandler<GetAllPositionsQuery, List<PositionProjection>>
    {
        private readonly UrbanDbContext _context;

    public GetAllPositionsQueryHandler(UrbanDbContext context)
    {
        _context = context;
    }

    public Task<List<PositionProjection>> Handle(GetAllPositionsQuery query)
    {
        return Task.FromResult(_context.PositionProjection.ToList());
    }

    }
}
