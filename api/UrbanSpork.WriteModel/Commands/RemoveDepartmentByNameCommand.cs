using System;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.WriteModel.Commands
{
    public class RemoveDepartmentByNameCommand : ICommand<DepartmentProjection>
    {
        public string name;

        public RemoveDepartmentByNameCommand(string Name)
        {
            name = Name;
        }
    }
    
}