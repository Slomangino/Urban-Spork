using Moq;
using System;
using System.Collections.Generic;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.Tests.Controllers.Department
{
    public class DepartmentControllerMockAggregate
    {
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public static readonly Mock<IQueryProcessor> QueryProcessorMock = new Mock<IQueryProcessor>();

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        private readonly IQueryProcessor QueryProcessor = QueryProcessorMock.Object;

        public ICommand<DepartmentProjection> DepartmentCommand;

        public IQuery<List<DepartmentProjection>> DepartmentQuery;

        public void setup_dispatcher_to_verify_createDepartmentCommands_are_the_same(CreateDepartmentCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreateDepartmentCommand>()))
                .Callback<ICommand<DepartmentProjection>>((a) => {DepartmentCommand = (CreateDepartmentCommand)a; })
                .ReturnsAsync(new DepartmentProjection()
                {
                    Name = command.Input.Name,
                });
        }

        public void setup_dispatcher_to_verify_removeDepartmentCommands_are_the_same(Guid id)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<RemoveDepartmentCommand>()))
                .Callback<ICommand<DepartmentProjection>>((a) => { DepartmentCommand = a; })
                .ReturnsAsync(new DepartmentProjection()
                {
                    Id = id,
                   
                });
        }

        public void setup_dispatcher_to_verify_removeDepartmentByNameCommands_are_the_same(string name)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<RemoveDepartmentByNameCommand>()))
                .Callback<ICommand<DepartmentProjection>>((a) => { DepartmentCommand = a; })
                .ReturnsAsync(new DepartmentProjection()
                {
                    Name = name
                });
        }

        public void setup_processor_to_verify_getDepartmentQuery_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetDepartmentsQuery>()))
                .Callback<IQuery<List<DepartmentProjection>>>((a) => { DepartmentQuery = a; })
                .ReturnsAsync(new List<DepartmentProjection>()
                {
                   

                });
        }

        public DepartmentController DepartmentControllerFactory()
        {
            return new DepartmentController(QueryProcessor, CommandDispatcher);
        }

        //public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        //{
        //    CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
        //        .Callback<ICommand<PermissionDTO>>((a) => { CreatePermissionCommand = a; })
        //        .ReturnsAsync(new PermissionDTO());
        //}
    }
}
