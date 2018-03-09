using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetApproverActivityProjectionQueryHandler : IQueryHandler<GetApproverActicityProjectionQuery, List<ApproverActivityProjection>>
    {
        private readonly UrbanDbContext _context;
        private string _searchTerm = "";

        public GetApproverActivityProjectionQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApproverActivityProjection>> Handle(GetApproverActicityProjectionQuery query)
        {
            return await Filter(query.FilterCriteria).ToListAsync();
        }

        private IQueryable<ApproverActivityProjection> Filter(ApproverActivityFilterCriteria filter)
        {
            IQueryable<ApproverActivityProjection> queryable = _context.ApproverActivityProjection;

           

            if (!String.IsNullOrWhiteSpace(filter.SearchTerms))
            {
                _searchTerm = filter.SearchTerms.ToLower();
                queryable = queryable.Where(
                    a => a.ForFullName.ToLower().Contains(_searchTerm) ||
                    a.PermissionName.ToLower().Contains(_searchTerm) ||
                    a.TruncatedEventType.ToLower().Contains(_searchTerm)
                );
            }

            if (filter.SortDirection.Equals("ASC"))
            {
                switch (filter.SortField)
                {
                    case "ApproverID":
                        queryable = queryable.OrderBy(a => a.ApproverId);
                        
                        break;
                    case "FullName":
                        queryable = queryable.OrderBy(a => a.ForFullName);
                       
                        break;
                    case "ForID":
                        queryable = queryable.OrderBy(a => a.ForId);
                        
                        break;
                    case "PermissionName":
                        queryable = queryable.OrderBy(a => a.PermissionName);
                        
                        break;
                    case "DateCreated":
                        queryable = queryable.OrderBy(a => a.TimeStamp);
                        
                        break;
                    case "Event":
                        queryable = queryable.OrderBy(a => a.TruncatedEventType);
                       
                        break;
                    
                    default:
                        queryable = queryable.OrderBy(a => a.ApproverId);
                        
                        break;
                }
            }
            else
            {
                switch (filter.SortField)
                {
                    case "ApproverID":
                        queryable = queryable.OrderByDescending(a => a.ApproverId);
                       
                        break;
                    case "FullName":
                        queryable = queryable.OrderByDescending(a => a.ForFullName);
                       
                        break;
                    case "ForID":
                        queryable = queryable.OrderByDescending(a => a.ForId);
                       
                        break;
                    case "PermissionName":
                        queryable = queryable.OrderByDescending(a => a.PermissionName);
                        
                        break;
                    case "DateCreated":
                        queryable = queryable.OrderByDescending(a => a.TimeStamp);
                        
                        break;
                    case "Event":
                        queryable = queryable.OrderByDescending(a => a.TruncatedEventType);
                       
                        break;

                    default:
                        queryable = queryable.OrderByDescending(a => a.ApproverId);
                       
                        break;
                }
            }

            //Filter For Min/Max Date Range
            if (filter.MinDate != null && filter.MaxDate != null)
            {
                queryable = queryable.Where(a =>
                    a.TimeStamp >= filter.MinDate &&
                    a.TimeStamp <= filter.MaxDate);
            }

            //Filter For MinDate Only
            else if (filter.MinDate != null && filter.MaxDate == null)
            {
                queryable = queryable.Where(a => a.TimeStamp >= filter.MinDate);
            }

            //Filter For MaxDate Only
            else if (filter.MinDate == null && filter.MaxDate != null)
            {
                queryable = queryable.Where(a => a.TimeStamp <= filter.MaxDate);
            }

            return queryable;
        }
    }
}