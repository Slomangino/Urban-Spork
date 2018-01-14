//using System;
//using System.Json;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UrbanSpork.Domain.Events.Users
//{
//    public class UserCreatedEvent : BaseEvent
//    {
//        public readonly int UserID;
//        public readonly string FirstName;
//        public readonly string LastName;
//        public readonly string Email;
//        public readonly string Position;
//        public readonly string Department;
//        public readonly Boolean IsActive;
//        public readonly JsonObject Assets;

//        public UserCreatedEvent(Guid id, int userID, string firstName, string lastName, string position, string department, Boolean isActive, JsonObject assets)
//        {
//            Id = id;
//            UserID = userID;
//            FirstName = firstName;
//            LastName = lastName;
//            Position = position;
//            Department = department;
//            IsActive = isActive;
//            Assets = assets;
//        }
//    }
//}