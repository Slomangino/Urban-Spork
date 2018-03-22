using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Emails
{
    public interface IEmail
    {
        void SendUserCreatedMessage(UserDTO user);
        void SendPermissionsRequestedMessage(UserAggregate forAgg, List<PermissionAggregate> permissions);
        void SendRequestDeniedMessage(UserAggregate forAgg, List<PermissionAggregate> deniedPermissions);
        void SendPermissionsGrantedMessage(UserAggregate forAgg, List<PermissionAggregate> grantedPermissions);
        void SendPermissionsRevokedMessage(UserAggregate forAgg, List<PermissionAggregate> revokedPermissions);
    }
}
