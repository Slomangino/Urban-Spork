using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class UpdateSingleUserPermissionsCommand : ICommand<UserDTO>
    {
        public UserPermissionsInputDTO Input { get; set; }

        public UpdateSingleUserPermissionsCommand(UserPermissionsInputDTO input)
        {
            Input = input;
        }
    }
}
