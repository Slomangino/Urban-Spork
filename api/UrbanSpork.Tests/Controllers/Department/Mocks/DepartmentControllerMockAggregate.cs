using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Commands;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.Tests.Controllers.Department.Mocks
{
    class DepartmentControllerMockAggregate
    {
    
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        //public ICommand<PermissionDTO> CreatePermissionCommand;

        public void setup_dispatcher_to_test_create_department_command(CreateDepartmentDTO dto)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreateDepartmentCommand>()))
                .Callback<CreateDepartmentCommand>(a => a.Input = dto)
                .ReturnsAsync(new DepartmentProjection());
        }
    }
}
