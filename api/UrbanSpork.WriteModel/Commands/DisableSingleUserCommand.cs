using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class DisableSingleUserCommand : ICommand<UserDTO>
    {
        public DisableUserInputDTO Input { get; set; }

        public DisableSingleUserCommand(DisableUserInputDTO input)
        {
            Input = input;
        }
    }
}
