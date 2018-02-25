using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserPermissionsRequestedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public Dictionary<Guid, PermissionRequest> Requests { get; set; } = new Dictionary<Guid, PermissionRequest>();

        public UserPermissionsRequestedEvent() { }

        public UserPermissionsRequestedEvent(UpdateUserPermissionsDTO dto)
        {
            foreach (var request in dto.Requests)
            {
                var r = new PermissionRequest
                {
                    EventType = JsonConvert.SerializeObject(GetType()),
                    IsPending = true,
                    ReasonForRequest = dto.Requests[request.Key].ReasonForRequest,
                    RequestDate = TimeStamp,
                    RequestedBy = dto.ById,
                    RequestedFor = dto.ForId
                };
                Requests[request.Key] = r;
            }
        }
    }
}
