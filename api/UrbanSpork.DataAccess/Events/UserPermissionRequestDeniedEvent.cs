using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events
{
    public class UserPermissionRequestDeniedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public Dictionary<Guid, PermissionDetails> PermissionsToDeny { get; set; } = new Dictionary<Guid, PermissionDetails>();

        public UserPermissionRequestDeniedEvent(){ }

        public UserPermissionRequestDeniedEvent(DenyUserPermissionRequestDTO dto)
        {
            ForId = dto.ForId;
            ById = dto.ById;
            foreach (var permission in dto.PermissionsToDeny)
            {
                var p = new PermissionDetails
                {
                    EventType = JsonConvert.SerializeObject(GetType().FullName),
                    IsPending = false,
                    Reason = "Denied", //IDK?
                    RequestDate = TimeStamp,
                    RequestedBy = dto.ById,
                    RequestedFor = dto.ForId
                };
                PermissionsToDeny[permission.Key] = p;
            }
        }

    }
}
