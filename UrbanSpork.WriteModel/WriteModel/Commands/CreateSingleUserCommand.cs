using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.WriteModel;

namespace UrbanSpork.WriteModel.Commands
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
