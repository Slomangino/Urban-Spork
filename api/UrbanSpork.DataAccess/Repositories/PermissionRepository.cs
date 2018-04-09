using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.DataAccess.Specifications.Permission;

namespace UrbanSpork.DataAccess.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly UrbanDbContext _context;
        private readonly IMapper _mapper;

        public PermissionRepository(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PermissionDTO> GetById(Guid id)
        {
            var getPermByIdSpec = new GetPermissionByIdSpecification(id);
            var permission = await _context.PermissionDetailProjection.SingleAsync(a => getPermByIdSpec.IsSatisfiedBy(a));
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task<List<PermissionDTO>> GetAllPermissions()
        {
            var result = await _context.PermissionDetailProjection.ToListAsync();
            var map = _mapper.Map<List<PermissionDetailProjection>, List<PermissionDTO>>(result);
            return map;
        }


    }
}
