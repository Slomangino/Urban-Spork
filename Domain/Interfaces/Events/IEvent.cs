using System;

namespace UrbanSpork.Domain.Interfaces.Events
{
    public interface IEvent
    {
        Guid Id { get; set; }
        int Version { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
