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
    public class GetSystemReportQueryHandler : IQueryHandler<GetSystemReportQuery, List<SystemActivityDTO>>
    {
        private readonly UrbanDbContext _context;

        public GetSystemReportQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<SystemActivityDTO>> Handle(GetSystemReportQuery query)
        {
            var filteredList = await Filter(query.FilterCriteria);
            var mappedResult = Mapper.Map<List<SystemActivityProjection>, List<SystemActivityDTO>>(filteredList);
            return mappedResult;
        }

        private async Task<List<SystemActivityProjection>> Filter(SystemReportFilterCriteria criteria)
        {
            IQueryable<SystemActivityProjection> query = _context.SystemActivityProjection;

            if (!String.IsNullOrWhiteSpace(criteria.SearchTerms))
            {
                criteria.SearchTerms = criteria.SearchTerms.ToLower();
                query = query.Where(a => a.ForFullName.ToLower().Contains(criteria.SearchTerms)
                                      || a.PermissionName.ToLower().Contains(criteria.SearchTerms));
            }

            if (criteria.SortDirection == "DSC")
            {
                if (criteria.SortField == "ForFullName") query = query.OrderByDescending(a => a.ForFullName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "PermissionName") query = query.OrderByDescending(a => a.PermissionName).ThenBy(a => a.Timestamp);
                else query = query.OrderByDescending(a => a.Timestamp);
            }
            else if (criteria.SortDirection == "ASC")
            {
                if (criteria.SortField == "ForFullName") query = query.OrderBy(a => a.ForFullName).ThenBy(a => a.Timestamp);
                else if (criteria.SortField == "PermissionName") query = query.OrderBy(a => a.PermissionName).ThenBy(a => a.Timestamp);
                else query = query.OrderBy(a => a.Timestamp);
            }
            else
            {
                query = query.OrderByDescending(a => a.ForFullName);
            }

            if (criteria.PermissionId != Guid.Empty)
            {
                query = query.Where(a => a.PermissionId == criteria.PermissionId);
            }

            //return all actions within a range that are less than the given date, default date is time of query
            //when querying, enter a local time, this will take care of conversion from utc to local
            query = query.Where(a => a.Timestamp <= criteria.EndDate);
            
            var list = await query.ToListAsync();

            List<SystemActivityProjection> result = new List<SystemActivityProjection>();

            foreach (var entry in list)
            {
                if (String.Equals(entry.EventType, "Permission Granted"))
                {
                    result.Add(entry);
                }
                if (String.Equals(entry.EventType, "Permission Revoked"))
                {
                    try
                    {
                        result.RemoveAll(a => a.ForId == entry.ForId && a.PermissionId == entry.PermissionId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            return result;
        }
    }
}
