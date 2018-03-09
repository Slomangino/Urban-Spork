using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class UpdatePermissionInfoCommand : ICommand<PermissionDTO>
    {
        public UpdatePermissionInfoDTO Input;

        public UpdatePermissionInfoCommand(UpdatePermissionInfoDTO input)
        {
            Input = input;
        }
    }
}
