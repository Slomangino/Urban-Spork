using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetUserDetailByIdQueryHandler : IQueryHandler<GetUserDetailByIdQuery, UserDetailProjectionDTO>
    {
        private readonly UrbanDbContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailByIdQueryHandler(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDetailProjectionDTO> Handle(GetUserDetailByIdQuery query)
        {
            var result = await _context.UserDetailProjection.Where(a => a.UserId == query.Id).SingleOrDefaultAsync();
            var mappedResult = _mapper.Map<UserDetailProjection, UserDetailProjectionDTO>(result);
            return mappedResult;
        }

    }
}
