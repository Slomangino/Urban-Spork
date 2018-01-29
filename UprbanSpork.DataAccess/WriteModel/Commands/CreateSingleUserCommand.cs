using System;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.Interfaces.WriteModel;

namespace UrbanSpork.DataAccess.WriteModel
{
    public class CreateSingleUserCommand : ICommand<UserDTO>
    {
        public UserDTO _input { get; set; }

        public CreateSingleUserCommand(UserDTO input)
        {
            _input = input;
        }
    }
}
