using CQRSlite.Domain;
using CQRSlite.Events;
using System;
using System.Collections.Generic;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public UserDTO userDTO { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        private UserAggregate() { }

        protected UserAggregate(UserDTO userDTO)
        {
            Id = userDTO.UserID;
            DateCreated = userDTO.DateCreated;
            ApplyChange(new UserCreatedEvent(userDTO));
        }

        //public UserAggregate(IEnumerable<IEvent> history)
        //{
        //    foreach (IEvent e in history)
        //    {
        //        ApplyEvent(e);
        //    }
        //}

        public static UserAggregate CreateNewUser(UserDTO userDTO)
        {
            var dto = userDTO;
            dto.UserID = Guid.NewGuid();
            dto.DateCreated = DateTime.Today;
            return new UserAggregate(dto);
        }

        //public static UserAggregate UpdateUser(UserDTO userDTO)
        //{
        //    var dto = userDTO;
        //    //wrong, needs to be changed to session.get<UserAggregate>(UserID); in userManager, all the aggregate does is apply events
        //    return new UserAggregate(dto);
        //}

        public void UpdateUser(UserInputDTO userInputDTO)
        {
            //wrong, needs to be changed to session.get<UserAggregate>(UserID); in userManager, all the aggregate does is apply events
            ApplyChange(new UserUpdatedEvent(userInputDTO));
        }

        private void Apply(UserCreatedEvent @event)
        {
            userDTO = @event.UserDTO;
        }

        private void Apply(UserUpdatedEvent @event)
        {
            userDTO =(UserDTO) @event.userInputDTO;
        }
    }
}
