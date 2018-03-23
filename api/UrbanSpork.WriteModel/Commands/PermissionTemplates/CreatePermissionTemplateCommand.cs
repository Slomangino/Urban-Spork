using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.WriteModel.Commands.PermissionTemplates
{
    public class CreatePermissionTemplateCommand : ICommand<PermissionTemplateDTO>
    {
        public CreatePermissionTemplateInputDTO Input;

        public CreatePermissionTemplateCommand(CreatePermissionTemplateInputDTO input)
        {
            Input = input;
        }
    }
}
