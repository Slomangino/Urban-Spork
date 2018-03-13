using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.WriteModel.Commands
{
    public class RemoveDepartmentCommand : ICommand<DepartmentProjection>
    {
        public Guid _id;

        public RemoveDepartmentCommand(Guid id)
        {
            _id = id;
        }
    }
}
