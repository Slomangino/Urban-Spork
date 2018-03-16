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
    public class GetDepartmentsQueryHandler : IQueryHandler<GetDepartmentsQuery, List<DepartmentProjection>>
    {
        private readonly UrbanDbContext _context;

        public GetDepartmentsQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public Task<List<DepartmentProjection>> Handle(GetDepartmentsQuery query)
        {
            return Task.FromResult(_context.DepartmentProjection.ToList());
        }
    
    }
}
