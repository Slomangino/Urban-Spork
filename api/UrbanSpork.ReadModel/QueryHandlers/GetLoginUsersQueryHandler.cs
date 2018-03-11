using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetLoginUsersQueryHandler : IQueryHandler<GetLoginUsersQuery, List<LoginUserDTO>>
    {
        private readonly UrbanDbContext _context;


        public GetLoginUsersQueryHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoginUserDTO>> Handle(GetLoginUsersQuery query)
        {
            var result = await  _context.UserDetailProjection.Where(user => user.IsActive.Equals(true)).ToListAsync();
            var mappedResult = Mapper.Map<List<UserDetailProjection>, List<LoginUserDTO>>(result);
            return mappedResult;
        }

        
    }
}
