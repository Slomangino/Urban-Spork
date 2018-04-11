using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands.Permission
{
    public class DisablePermissionCommand : ICommand<PermissionDTO>
    {
        public DisablePermissionInputDTO Input;

        public DisablePermissionCommand(DisablePermissionInputDTO input)
        {
            Input = input;
        }
    }
}
