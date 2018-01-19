using System;
using UrbanSpork.Domain.DataTransferObjects;
using UrbanSpork.Domain.SLCQRS.WriteModel;

namespace UrbanSpork.Domain.WriteModel.Commands
{
    public class CreateSingleUserCommand : ICommand<UserDTO>
    {
        UserDTO _input { get; set; }

        public CreateSingleUserCommand(UserDTO input)
        {
            _input = input;
        }
    }
}
