using System;
using System.Collections.Generic;
using System.Text;
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
        public Guid PermissionId { get; set; }
        public string ReasonForDenial { get; set; }

        public UserPermissionRequestDeniedEvent(){ }

        public UserPermissionRequestDeniedEvent(DenyUserPermissionRequestDTO dto)
        {
            ForId = dto.ForId;
            ById = dto.ById;
            PermissionId = dto.PermissionId;
            ReasonForDenial = dto.ReasonForDenial;
        }

    }
}
