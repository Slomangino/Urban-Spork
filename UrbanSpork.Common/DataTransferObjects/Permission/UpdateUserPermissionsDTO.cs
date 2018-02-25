using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Permission
{
    public class UpdateUserPermissionsDTO
    {
        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public Dictionary<Guid, PermissionRequest> Requests { get; set; }
    }
}
