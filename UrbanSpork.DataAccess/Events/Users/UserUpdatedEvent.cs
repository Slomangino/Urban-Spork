using CQRSlite.Events;
using System;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserUpdatedEvent : IEvent
    {
        public UserInputDTO userInputDTO { get; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; } = 0;
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public UserUpdatedEvent(UserInputDTO userInputDTO)
        {
            this.userInputDTO = userInputDTO;
        }
    }
}
