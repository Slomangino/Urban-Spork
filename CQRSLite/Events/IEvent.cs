using CQRSlite.Messages;
using System;

namespace CQRSlite.Events
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
