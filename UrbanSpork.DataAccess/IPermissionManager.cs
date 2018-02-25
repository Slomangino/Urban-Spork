using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;

namespace UrbanSpork.DataAccess
{
    public interface IPermissionManager
    {
        Task<PermissionDTO> CreateNewPermission(CreateNewPermissionDTO input);
    }
}
