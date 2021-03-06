﻿using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events
{
    public class PermissionDisabledEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsActive { get; set; }
        public Guid ById { get; set; }

        public PermissionDisabledEvent() { }

        public PermissionDisabledEvent(Guid byId)
        {
            IsActive = false;
            ById = byId;
        }
    }
}
