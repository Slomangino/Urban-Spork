using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserPermissionsRequestedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public Dictionary<Guid, PermissionDetails> Requests { get; set; } = new Dictionary<Guid, PermissionDetails>();

        public UserPermissionsRequestedEvent() { }

        public UserPermissionsRequestedEvent(UpdateUserPermissionsDTO dto)
        {
            foreach (var request in dto.Requests)
            {
                var r = new PermissionDetails
                {
                    EventType = JsonConvert.SerializeObject(GetType().FullName),
                    IsPending = true,
                    Reason = dto.Requests[request.Key].Reason,
                    RequestDate = TimeStamp,
                    RequestedBy = dto.ById,
                    RequestedFor = dto.ForId
                };
                Requests[request.Key] = r;
            }
        }
    }
}
