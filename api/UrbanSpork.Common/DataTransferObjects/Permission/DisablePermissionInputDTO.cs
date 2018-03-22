using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Permission
{
    public class DisablePermissionInputDTO
    {
        public Guid PermissionId { get; set; }
        public Guid ById { get; set; }
    }
}
