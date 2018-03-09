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
   public class GetSystemDropDownProjectionQueryHandler : IQueryHandler<GetSystemDropDownProjectionQuery, List<SystemDropdownProjection>>
    {
        private UrbanDbContext _context;

        public GetSystemDropDownProjectionQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }



        public async Task<List<SystemDropdownProjection>> Handle(GetSystemDropDownProjectionQuery query)
        {
            return await _context.SystemDropDownProjection.ToListAsync();
        }
    }
}
