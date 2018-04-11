using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetApproverListQueryHandler : IQueryHandler<GetApproverListQuery, List<ApproverListDTO>>
    {
        public readonly UrbanDbContext _context;
        public readonly IMapper _mapper;

        public GetApproverListQueryHandler(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ApproverListDTO>> Handle(GetApproverListQuery query)
        {
            var approverList = await Filter(query.Filter).Select(a => new ApproverListDTO { Id = a.ApproverId, Name = a.ApproverFullName })
                .ToListAsync();

            // Distinct by Extension method, returns enumerable distinct by a certain property
            // https://stackoverflow.com/questions/489258/linqs-distinct-on-a-particular-property
            approverList = approverList.DistinctBy(a => a.Id).ToList();
            return approverList;
        }

        public IQueryable<ApproverActivityProjection> Filter(GetApproverListFilterCriteria filter)
        {
            var query = _context.ApproverActivityProjection;
            return query;
        }
    }
}
