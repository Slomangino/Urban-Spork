using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class UpdateUserPermissionsCommand : ICommand<UserDTO>
    {
        public UpdateUserPermissionsDTO Input { get; set; }

        public UpdateUserPermissionsCommand(UpdateUserPermissionsDTO input)
        {
            Input = input;
        }
    }
}
