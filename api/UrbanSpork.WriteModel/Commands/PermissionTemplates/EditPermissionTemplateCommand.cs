using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands.PermissionTemplates
{
    public class EditPermissionTemplateCommand : ICommand<PermissionTemplateDTO>
    {
        public EditPermissionTemplateInputDTO Input;

        public EditPermissionTemplateCommand(EditPermissionTemplateInputDTO input)
        {
            this.Input = input;
        }
    }
}
