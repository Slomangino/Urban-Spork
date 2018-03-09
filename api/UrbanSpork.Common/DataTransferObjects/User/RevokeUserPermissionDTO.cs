using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class RevokeUserPermissionDTO
    {
        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public Dictionary<Guid, PermissionDetails> PermissionsToRevoke { get; set; } = new Dictionary<Guid, PermissionDetails>();
    }
}
