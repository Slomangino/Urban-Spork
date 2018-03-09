using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class RevokeUserPermissionCommand : ICommand<UserDTO>
    {
        public RevokeUserPermissionDTO Input;

        public RevokeUserPermissionCommand(RevokeUserPermissionDTO input)
        {
            Input = input;
        }
    }
}
