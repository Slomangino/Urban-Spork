using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands.User
{
    public class EnableSingleUserCommand : ICommand<UserDTO>
    {
        public EnableUserInputDTO Input { get; set; }

        public EnableSingleUserCommand(EnableUserInputDTO input)
        {
            Input = input;
        }
    }
}
