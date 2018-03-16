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
    public class GetPositionsByDepartmentNameQueryHandler : IQueryHandler<GetPositionsByDepartmentNameQuery, List<PositionProjection>>
    {
        private readonly UrbanDbContext _context;

        public GetPositionsByDepartmentNameQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public Task<List<PositionProjection>> Handle(GetPositionsByDepartmentNameQuery query)
        {
            return Task.FromResult(_context.PositionProjection.Where(p => p.DepartmentName.Equals(query.departmentName, StringComparison.CurrentCultureIgnoreCase)).ToList());
        }

    }
}
