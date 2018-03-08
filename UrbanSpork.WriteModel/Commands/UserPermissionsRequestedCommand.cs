using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class UserPermissionsRequestedCommand : ICommand<UserDTO>
    {
        public RequestUserPermissionsDTO Input { get; set; }

        public UserPermissionsRequestedCommand(RequestUserPermissionsDTO input)
        {
            Input = input;
        }
    }
}
