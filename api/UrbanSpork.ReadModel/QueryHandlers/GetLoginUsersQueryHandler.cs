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
        private readonly IMapper _mapper;


        public GetLoginUsersQueryHandler(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LoginUserDTO>> Handle(GetLoginUsersQuery query)
        {
            var result = await  _context.UserDetailProjection.Where(user => user.IsActive.Equals(true)).ToListAsync();
            var mappedResult = _mapper.Map<List<UserDetailProjection>, List<LoginUserDTO>>(result);
            return mappedResult;
        }

        
    }
}
