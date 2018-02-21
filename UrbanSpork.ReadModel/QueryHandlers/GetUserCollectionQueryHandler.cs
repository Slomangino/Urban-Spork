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
        private readonly UrbanDbContext _context;

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
            //    queryable = queryable.Where(a => a.Department.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.FirstName.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.LastName.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.Position.Contains(query.FilterCriteria.SearchTerms));
            //    queryable = queryable.Where(a => a.Department.Contains(query.FilterCriteria.SearchTerms));
            //}

            if (query.FilterCriteria.SortDirection.Equals("DESC"))
            {
                switch (query.FilterCriteria.SortField)
                {
                    case "Email":
                        queryable = queryable.OrderBy(a => a.Email);
                        queryable = queryable.OrderBy(a => a.FirstName);
                        break;
                    case "LastName":
                        queryable = queryable.OrderBy(a => a.LastName);
                        queryable = queryable.OrderBy(a => a.FirstName);
                        break;
                    case "IsAdmin":
                        queryable = queryable.OrderBy(a => a.IsAdmin);
                        queryable = queryable.OrderBy(a => a.FirstName);
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                    case "DateCreated":
                        queryable = queryable.OrderBy(a => a.DateCreated);
                        queryable = queryable.OrderBy(a => a.FirstName);
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                    case "Position":
                        queryable = queryable.OrderBy(a => a.Position);
                        queryable = queryable.OrderBy(a => a.FirstName);
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                    case "Department":
                        queryable = queryable.OrderBy(a => a.Department);
                        queryable = queryable.OrderBy(a => a.FirstName);
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                    default:
                        queryable = queryable.OrderBy(a => a.FirstName);
                        queryable = queryable.OrderBy(a => a.LastName);
                        break;
                }
            }
            else
            {
                switch (query.FilterCriteria.SortField)
                {
                    case "Email":
                        queryable = queryable.OrderByDescending(a => a.Email);
                        break;
                    case "LastName":
                        queryable = queryable.OrderByDescending(a => a.LastName);
                        break;
                    case "IsAdmin":
                        queryable = queryable.OrderByDescending(a => a.IsAdmin);
                        break;
                    case "DateCreated":
                        queryable = queryable.OrderByDescending(a => a.DateCreated);
                        break;
                    case "Position":
                        queryable = queryable.OrderByDescending(a => a.Position);
                        break;
                    case "Department":
                        queryable = queryable.OrderByDescending(a => a.Department);
                        break;
                    default:
                        queryable = queryable.OrderByDescending(a => a.FirstName);
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