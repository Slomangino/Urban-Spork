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
    public class GetUserCollectionQueryHandler : IQueryHandler<GetUserCollectionQuery, List<UserDTO>>
    {
        private UrbanDbContext _context;

        public GetUserCollectionQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public Task<List<UserDTO>> Handle(GetUserCollectionQuery query)
        {
            IQueryable<UserDetailProjection> queryable = _context.UserDetailProjection;

            if (!query.FilterCriteria.IncludeInactive)
            {
                queryable = queryable.Where(a => a.IsActive);
            }

            //if (!query.FilterCriteria.SearchTerms.Equals(""))
            //{
            //    queryable = queryable.Contains<UserDetailProjection>(query.FilterCriteria.SearchTerms));
            //}

            if (query.FilterCriteria.SortDirection.Equals("ASC"))
            {
                switch (query.FilterCriteria.SortField)
                {
                    case "Email":
                        queryable.OrderBy(a => a.Email);
                        break;
                    case "LastName":
                        queryable.OrderBy(a => a.LastName);
                        break;
                    case "IsAdmin":
                        queryable.OrderBy(a => a.IsAdmin);
                        break;
                    case "DateCreated":
                        queryable.OrderBy(a => a.DateCreated);
                        break;
                    case "Position":
                        queryable.OrderBy(a => a.Position);
                        break;
                    case "Department":
                        queryable.OrderBy(a => a.Department);
                        break;
                    default:
                        queryable.OrderBy(a => a.FirstName);
                        break;
                }
            }
            else
            {
                switch (query.FilterCriteria.SortField)
                {
                    case "Email":
                        queryable.OrderByDescending(a => a.Email);
                        break;
                    case "LastName":
                        queryable.OrderByDescending(a => a.LastName);
                        break;
                    case "IsAdmin":
                        queryable.OrderByDescending(a => a.IsAdmin);
                        break;
                    case "DateCreated":
                        queryable.OrderByDescending(a => a.DateCreated);
                        break;
                    case "Position":
                        queryable.OrderByDescending(a => a.Position);
                        break;
                    case "Department":
                        queryable.OrderByDescending(a => a.Department);
                        break;
                    default:
                        queryable.OrderByDescending(a => a.FirstName);
                        break;
                }
            }

            var result = queryable.ToList();
            var userDtoCollection = Mapper.Map(result, new List<UserDTO>());

            return Task.FromResult(userDtoCollection);

            //throw new NotImplementedException();
        }
    }
}