using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetUserManagementProjectionQueryHandler : IQueryHandler<GetUserManagementProjectionQuery, List<UserManagementDTO>>
    {
        private readonly UrbanDbContext _context;

        public GetUserManagementProjectionQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserManagementDTO>> Handle(GetUserManagementProjectionQuery query)
        {

            var mappedResult = Mapper.Map<List<UserManagementProjection>, List<UserManagementDTO>>(await Filter(query.FilterCriteria).ToListAsync());
            return mappedResult;

        }

        private IQueryable<UserManagementProjection> Filter(UserFilterCriteria filter)
        {
            IQueryable<UserManagementProjection> queryable = _context.UserManagementProjection;

            if (!filter.IncludeInactive)
            {
                queryable = queryable.Where(a => a.IsActive);
            }

            if (!filter.IncludeIsAdmin)
            {
                queryable = queryable.Where(a => !a.IsAdmin);
            } 

            if (filter.SortDirection.Equals("ASC"))
            {
                switch (filter.SortField)
                {
                    case "Email":
                        queryable = queryable.OrderBy(a => a.Email);
                        break;
                    case "LastName":
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                    case "FirstName":
                        queryable = queryable.OrderBy(a => a.FirstName);
                        break;
                    case "IsAdmin":
                        queryable = queryable.OrderBy(a => a.IsAdmin);
                        break;
                    case "Position":
                        queryable = queryable.OrderBy(a => a.Position);
                        break;
                    case "Department":
                        queryable = queryable.OrderBy(a => a.Department);
                        break;
                    default:
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                }
            }
            else
            {
                switch (filter.SortField)
                {
                    case "Email":
                        queryable = queryable.OrderByDescending(a => a.Email);
                        break;
                    case "LastName":
                        queryable = queryable.OrderByDescending(a => a.LastName);
                        break;
                    case "FirstName":
                        queryable = queryable.OrderByDescending(a => a.FirstName);
                        break;
                    case "IsAdmin":
                        queryable = queryable.OrderByDescending(a => a.IsAdmin);
                        break;
                    case "Position":
                        queryable = queryable.OrderByDescending(a => a.Position);
                        break;
                    case "Department":
                        queryable = queryable.OrderByDescending(a => a.Department);
                        break;
                    default:
                        queryable = queryable.OrderByDescending(a => a.LastName);
                        break;
                }
            }


                switch (filter.FilterField)
                {
                    case "Email":
                        queryable = queryable.Where(a => a.Email.ToLower().Equals(filter.FilterFieldInput.ToLower()));
                        break;
                    case "LastName":
                        queryable = queryable.Where(a => a.LastName.ToLower().Equals(filter.FilterFieldInput.ToLower()));
                    break;
                    case "FirstName":
                        queryable = queryable.Where(a => a.FirstName.ToLower().Equals(filter.FilterFieldInput.ToLower()));
                    break;
                    case "Position":
                        queryable = queryable.Where(a => a.Position.ToLower().Equals(filter.FilterFieldInput.ToLower()));
                    break;
                    case "Department":
                        queryable = queryable.Where(a => a.Department.ToLower().Equals(filter.FilterFieldInput.ToLower()));
                    break;
                    default:
                    break;
                }

            return queryable;
        }
    }
}
