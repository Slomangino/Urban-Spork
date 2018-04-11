using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetTemplatesQueryHandler : IQueryHandler<GetTemplatesQuery, List<PermissionTemplateProjection>>
    {
        private readonly UrbanDbContext _context;

        public GetTemplatesQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }
        public async Task<List<PermissionTemplateProjection>> Handle(GetTemplatesQuery query)
        {
            return await _context.PermissionTemplateProjection.ToListAsync();
        }
    }
}