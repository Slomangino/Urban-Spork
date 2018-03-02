using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class DisablePermissionCommand : ICommand<PermissionDTO>
    {
        public DisablePermissionDTO Input;

        public DisablePermissionCommand(DisablePermissionDTO input)
        {
            Input = input;
        }
    }
}
