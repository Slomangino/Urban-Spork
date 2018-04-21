using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands.PermissionActions
{
    public class GrantRevokePermissionsCommand : ICommand<UserDTO>
    {
        public GrantRevokePermissionsInputDTO Input;

        public GrantRevokePermissionsCommand(GrantRevokePermissionsInputDTO input)
        {
            Input = input;
        }
    }
}
