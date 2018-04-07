using System;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.Tests.CQRS.Substitutes
{
    public class TestAggregateCreated : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        DateTime IEvent.TimeStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}