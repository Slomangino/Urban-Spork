using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.WriteModel.Commands
{
    public class CreateDepartmentCommand : ICommand<DepartmentProjection>
    {
        public CreateDepartmentDTO Input { get; set; }

        public CreateDepartmentCommand(CreateDepartmentDTO input)
        {
            Input = input;
        }
    }
}
