using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetUserCollectionQueryHandler : IQueryHandler<GetUserCollectionQuery, List<UserDetailProjection>>
    {
        private readonly UrbanDbContext _context;

        public GetUserCollectionQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDetailProjection>> Handle(GetUserCollectionQuery query)
        {
            IQueryable<UserDetailProjection> queryable = _context.UserDetailProjection;

            if (!query.FilterCriteria.IncludeInactive)
            {
                queryable = queryable.Where(a => a.IsActive);
            }

            if (!query.FilterCriteria.IncludeIsAdmin)
            {
                queryable = queryable.Where(a => a.IsAdmin);
            }


            //if (!query.FilterCriteria.SearchTerms.Equals(""))
            //{
            //    queryable = queryable.Where(a => a.Department.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.FirstName.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.LastName.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.Position.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.Department.Contains(query.FilterCriteria.SearchTerms));
            //}



            if (query.FilterCriteria.SortDirection.Equals("ASC"))
            {
                switch (query.FilterCriteria.SortField)
                {
                    case "Email":
                        queryable = queryable.OrderBy(a => a.Email)
                            .ThenBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                        break;
                    case "LastName":
                        queryable = queryable.OrderBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                        break;
                    case "FirstName":
                        queryable = queryable.OrderBy(a => a.FirstName)
                            .ThenBy(a => a.LastName);
                        break;
                    case "IsAdmin":
                        queryable = queryable.OrderBy(a => a.IsAdmin)
                            .ThenBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                        break;
                    case "DateCreated":
                        queryable = queryable.OrderBy(a => a.DateCreated)
                            .ThenBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                        break;
                    case "Position":
                        queryable = queryable.OrderBy(a => a.Position)
                            .ThenBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                        break;
                    case "Department":
                        queryable = queryable.OrderBy(a => a.Department)
                            .ThenBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                            
                        break;
                    default:
                        queryable = queryable.OrderBy(a => a.LastName)
                            .ThenBy(a => a.FirstName);
                        break;
                }
            }
            else
            {
                switch (query.FilterCriteria.SortField)
                {
                    case "Email":
                        queryable = queryable.OrderByDescending(a => a.Email)
                            .ThenByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                    case "LastName":
                        queryable = queryable.OrderByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                    case "FirstName":
                        queryable = queryable.OrderByDescending(a => a.FirstName)
                            .ThenByDescending(a => a.LastName);
                        break;
                    case "IsAdmin":
                        queryable = queryable.OrderByDescending(a => a.IsAdmin)
                            .ThenByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                    case "DateCreated":
                        queryable = queryable.OrderByDescending(a => a.DateCreated)
                            .ThenByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                    case "Position":
                        queryable = queryable.OrderByDescending(a => a.Position)
                            .ThenByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                    case "Department":
                        queryable = queryable.OrderByDescending(a => a.Department)
                            .ThenByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                    default:
                        queryable = queryable.OrderByDescending(a => a.LastName)
                            .ThenByDescending(a => a.FirstName);
                        break;
                }
            }

            var result = await queryable.ToListAsync();

            
            //var map = Mapper.Map<List<UserDetailProjection>, List<UserDTO>>(result);
            //return map;

            return result;

            //throw new NotImplementedException();
        }
    }
}