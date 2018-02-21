using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserPermissionsInputDTO
    {
        public Guid Id { get; set; }

        public Dictionary<Permissions, PermissionStatus> PermissionList { get; set; }

    }
}
