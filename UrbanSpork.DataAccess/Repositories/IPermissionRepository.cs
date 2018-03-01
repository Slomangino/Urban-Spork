using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;

namespace UrbanSpork.DataAccess.Repositories
{
    public interface IPermissionRepository
    {
        Task<PermissionDTO> GetById(Guid id);
        Task<List<PermissionDTO>> GetAllPermissions();
    }
}
