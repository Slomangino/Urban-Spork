using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.DataAccess.Specifications.Permission;

namespace UrbanSpork.DataAccess.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly UrbanDbContext _context;

        public PermissionRepository(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionDTO> GetById(Guid id)
        {
            var getPermByIdSpec = new GetPermissionByIdSpecification(id);
            var permission = await _context.PermissionDetailProjection.SingleAsync(a => getPermByIdSpec.IsSatisfiedBy(a));
            return Mapper.Map<PermissionDTO>(permission);
        }

        public async Task<List<PermissionDTO>> GetAllPermissions()
        {
            var result = await _context.PermissionDetailProjection.ToListAsync();
            var map = Mapper.Map<List<PermissionDetailProjection>, List<PermissionDTO>>(result);
            return map;
        }


    }
}
