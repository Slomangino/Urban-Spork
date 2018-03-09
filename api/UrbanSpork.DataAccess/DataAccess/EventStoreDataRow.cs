using System;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class EventStoreDataRow
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; } 
        public string EventData { get; set; } 
    }
}
