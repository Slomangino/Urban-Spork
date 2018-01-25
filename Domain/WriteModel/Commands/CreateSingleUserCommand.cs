using System;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.Interfaces.WriteModel;

namespace UrbanSpork.Domain.WriteModel.Commands
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
