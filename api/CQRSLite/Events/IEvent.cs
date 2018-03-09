using System;
using UrbanSpork.CQRS.Messages;

namespace UrbanSpork.CQRS.Events
{
    /// <summary>
    /// Defines an event with required fields.
    /// </summary>
    public interface IEvent : IMessage
    {
        Guid Id { get; set; }
        int Version { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
