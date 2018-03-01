using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class UserPermissionsRequestedCommand : ICommand<UserDTO>
    {
        public UpdateUserPermissionsDTO Input { get; set; }

        public UserPermissionsRequestedCommand(UpdateUserPermissionsDTO input)
        {
            Input = input;
        }
    }
}
