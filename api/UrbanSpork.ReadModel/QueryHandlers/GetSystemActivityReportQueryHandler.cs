using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetSystemActivityReportQueryHandler : IQueryHandler<GetSystemActivityReportQuery, List<SystemActivityDTO>>
    {
        private readonly UrbanDbContext _context;
        private readonly IMapper _mapper;


        public GetSystemActivityReportQueryHandler(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<SystemActivityDTO>> Handle(GetSystemActivityReportQuery query)
        {
            var filteredList = await Filter(query.FilterCriteria).ToListAsync();
            var mappedResult = _mapper.Map<List<SystemActivityProjection>, List<SystemActivityDTO>>(filteredList);
            return mappedResult;
        }

        private IQueryable<SystemActivityProjection> Filter(SystemActivityReportFilterCriteria criteria)
        {
            IQueryable<SystemActivityProjection> query = _context.SystemActivityProjection;

            if (!String.IsNullOrWhiteSpace(criteria.SearchTerms))
            {
                criteria.SearchTerms = criteria.SearchTerms.ToLower();
                query = query.Where(a => a.ByFullName.ToLower().Contains(criteria.SearchTerms)
                                      || a.ForFullName.ToLower().Contains(criteria.SearchTerms)
                                      || a.PermissionName.ToLower().Contains(criteria.SearchTerms));
            }

            if (criteria.SortDirection == "DSC")
            {
                if (criteria.SortField == "ForFullName") query = query.OrderByDescending(a => a.ForFullName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "ByFullName") query = query.OrderByDescending(a => a.ByFullName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "PermissionName") query = query.OrderByDescending(a => a.PermissionName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "Timestamp") query = query.OrderByDescending(a => a.Timestamp);
                else query = query.OrderByDescending(a => a.Timestamp);
            }
            else if(criteria.SortDirection == "ASC")
            {
                if (criteria.SortField == "ForFullName") query = query.OrderBy(a => a.ForFullName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "ByFullName") query = query.OrderBy(a => a.ByFullName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "PermissionName") query = query.OrderBy(a => a.PermissionName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "Timestamp") query = query.OrderBy(a => a.Timestamp);
                else query = query.OrderBy(a => a.Timestamp);
            }
            else
            {
                query = query.OrderByDescending(a => a.Timestamp);
            }

            //return all actions within a range, default is time 0 and returns all events
            query = query.Where(a => a.Timestamp.ToLocalTime() >= criteria.StartDate);
           
            //when querying, enter a local time, this will take care of conversion from utc to local
            if (criteria.EndDate != null)
            {
                query = query.Where(a => a.Timestamp.ToLocalTime() <= criteria.EndDate);
            }
            

            return query;
        }
    }
}
