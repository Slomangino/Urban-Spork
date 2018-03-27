using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserPermissionRevokedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public Dictionary<Guid, PermissionDetails> PermissionsToRevoke { get; set; } = new Dictionary<Guid, PermissionDetails>();

        public UserPermissionRevokedEvent() { }

        public UserPermissionRevokedEvent(RevokeUserPermissionDTO dto)
        {
            ForId = dto.ForId;
            ById = dto.ById;
            foreach (var permission in dto.PermissionsToRevoke)
            {
                var reason = String.IsNullOrWhiteSpace(permission.Value.Reason) ?  "Reason Not Specified":  permission.Value.Reason;

                var p = new PermissionDetails
                {
                    EventType = JsonConvert.SerializeObject(GetType().FullName),
                    IsPending = false,
                    Reason = reason, 
                    RequestDate = TimeStamp,
                    RequestedBy = dto.ById,
                    RequestedFor = dto.ForId
                };
                PermissionsToRevoke[permission.Key] = p;
            }
        }
    }
}
