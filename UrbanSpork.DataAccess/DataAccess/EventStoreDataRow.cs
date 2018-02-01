using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class EventStoreDataRow
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; } = 2;
        public string EventType { get; set; } = "WHATEVER IT DOESN'T MATTER";
        public string EventData { get; set; } = "BIG-TEXT SO IT CAN FIT ALL YOUR JSON OBJECT";
    }
}
