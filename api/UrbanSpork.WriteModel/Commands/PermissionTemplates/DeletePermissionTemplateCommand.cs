using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands.PermissionTemplates
{
    public class DeletePermissionTemplateCommand : ICommand<string>
    {
        public DeletePermissionTemplateInputDTO Input { get; set; }

        public DeletePermissionTemplateCommand(DeletePermissionTemplateInputDTO input)
        {
            Input = input;
        }
    }
}
