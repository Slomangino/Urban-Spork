using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.Commands
{
    public class CreateUserCommand : BaseCommand
    {
        public readonly int UserID;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Email;
        public readonly string Position;
        public readonly string Department;
        public readonly Boolean IsActive;
        public readonly JsonObject Assets;

        public CreateUserCommand(Guid id, int userID, string firstName, string lastName, string email, string position, string department, Boolean isActive, System.Json.JsonObject assets)
        {
            Id = id;
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Position = position;
            Department = department;
            IsActive = isActive;
            Assets = assets;
        }
    }
}