using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands.PermissionActions
{
    public class GrantUserPermissionCommand : ICommand<UserDTO>
    {
        public GrantUserPermissionDTO Input;

        public GrantUserPermissionCommand(GrantUserPermissionDTO input)
        {
            Input = input;
        }
    }
}
