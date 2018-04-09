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
    public class GetOffboardUserPermissionsQueryHandler : IQueryHandler<GetOffboardUserPermissionsQuery, OffBoardUserDTO>
    {
        private readonly UrbanDbContext _context;
        private readonly IMapper _mapper;


        public GetOffboardUserPermissionsQueryHandler(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OffBoardUserDTO> Handle(GetOffboardUserPermissionsQuery query)
        {
            var result = await _context.UserDetailProjection.Where(a => a.UserId == query.UserID).SingleOrDefaultAsync();
            var mappedResult = _mapper.Map<UserDetailProjection, OffBoardUserDTO>(result);
            


            return FilterList(mappedResult);     
        }

        public OffBoardUserDTO FilterList(OffBoardUserDTO permissions)
        {
            var list = permissions.PermissionList;
            if (list.Keys.Count != 0)
            {
                foreach (var p in list.ToList())
                {
                    if (!(p.Value.PermissionStatus == "Granted"))
                    {
                        list.Remove(p.Key);
                    }
                }

                var filteredList = new OffBoardUserDTO
                {
                    PermissionList = list,
                };

                return filteredList;

            }
            else
            {
                return permissions;
            }

           
        }
    }
}
