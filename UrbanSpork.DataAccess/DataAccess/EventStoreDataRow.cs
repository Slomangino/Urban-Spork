using System;
using System.ComponentModel.DataAnnotations;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class EventStoreDataRow
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; } = "WHATEVER IT DOESN'T MATTER";
        public string EventData { get; set; } = "BIG-TEXT SO IT CAN FIT ALL YOUR JSON OBJECT";
    }
}
