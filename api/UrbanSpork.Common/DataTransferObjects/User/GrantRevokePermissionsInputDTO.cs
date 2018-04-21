using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class GrantRevokePermissionsInputDTO
    {
        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public Dictionary<Guid, PermissionDetails> Permissions { get; set; } = new Dictionary<Guid, PermissionDetails>();
    }
}
