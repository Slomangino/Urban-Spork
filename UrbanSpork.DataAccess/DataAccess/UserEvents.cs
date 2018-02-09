using CQRSlite.Events;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class UserEvents : IEvent
    {
        [Key]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EventType { get; set; }

        [Column(TypeName = "json")]
        public string OldUserDTO { get; set; }

        [Column(TypeName = "json")]
        public string UpdatedUserDTO { get; set; }
    }
}
