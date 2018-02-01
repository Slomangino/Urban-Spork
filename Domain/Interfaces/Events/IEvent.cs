using System;

namespace UrbanSpork.CQRS.Interfaces.Events
{
    public interface IEvent
    {
        Guid Id { get; set; }
        int Version { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
