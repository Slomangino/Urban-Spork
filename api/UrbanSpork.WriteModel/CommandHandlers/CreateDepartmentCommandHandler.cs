using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, DepartmentDTO>
    {
        private readonly IDepartmentManager _departmentManager;

        public CreateDepartmentCommandHandler(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        public async Task<DepartmentDTO> Handle(CreateDepartmentCommand command)
        {
            var DepartmentDTO = await _departmentManager.CreateNewDepartmentAsync(command.Input);
            return DepartmentDTO;
        }
    }
}
