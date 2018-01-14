//using CQRSlite.Domain;
//using UrbanSpork.Domain.Events.Users;
//using System;
//using System.Json;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UrbanSpork.Domain.WriteModel
//{
//    public class User : AggregateRoot
//    {
//        private int _userID;
//        private string _firstName;
//        private string _lastName;
//        private string _email;
//        private string _position;
//        private string _department;
//        private Boolean _isActive;
//        private JsonObject _assets;

//        private User() { }

//        public User(Guid id, int userID, string firstName, string lastName, string email, string position, string department, Boolean isActive, JsonObject assets)
//        {
//            Id = id;
//            _userID = userID;
//            _firstName = firstName;
//            _lastName = lastName;
//            _email = email;
//            _position = position;
//            _department = department;
//            _isActive = isActive;
//            _assets = assets;

//            ApplyChange(new UserCreatedEvent(id, userID, firstName, lastName, position, department, isActive, assets));
//        }
//    }
//}