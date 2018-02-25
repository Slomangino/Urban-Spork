using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class CreatePermissionCommand : ICommand<PermissionDTO>
    {
        public CreateNewPermissionDTO Input { get; set; }

        public CreatePermissionCommand(CreateNewPermissionDTO input)
        {
            Input = input;
        }
    }
}
