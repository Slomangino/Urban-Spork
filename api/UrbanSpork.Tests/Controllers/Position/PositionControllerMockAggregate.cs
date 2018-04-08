using Moq;
using System;
using System.Collections.Generic;
using UrbanSpork.API.Controllers;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.Tests.Controllers.Position
{
    public class PositionControllerMockAggregate
    {
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public static readonly Mock<IQueryProcessor> QueryProcessorMock = new Mock<IQueryProcessor>();

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        private readonly IQueryProcessor QueryProcessor = QueryProcessorMock.Object;

        public ICommand<PositionProjection> PositonCommand;

        public IQuery<List<PositionProjection>> PositionQuery;

        public void setup_dispatcher_to_verify_createPositionCommands_are_the_same(CreatePositionCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePositionCommand>()))
                .Callback<ICommand<PositionProjection>>((a) => { PositonCommand = (CreatePositionCommand)a; })
                .ReturnsAsync(new PositionProjection()
                {
                    PositionName = command.Input.PositionName,
                    
                });
        }

        public void setup_dispatcher_to_verify_removePositionCommands_are_the_same(Guid id)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<RemovePositionCommand>()))
                .Callback<ICommand<PositionProjection>>((a) => { PositonCommand = a; })
                .ReturnsAsync(new PositionProjection()
                {
                    Id = id,
                   
                });
        }

        public void setup_processor_to_verify_getPositionQuery_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetAllPositionsQuery>()))
                .Callback<IQuery<List<PositionProjection>>>((a) => { PositionQuery = a; })
                .ReturnsAsync(new List<PositionProjection>()
                {
                   

                });
        }

        public void setup_processor_to_verify_getPositionByDepartmentQuery_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetPositionsByDepartmentNameQuery>()))
                .Callback<IQuery<List<PositionProjection>>>((a) => { PositionQuery = a; })
                .ReturnsAsync(new List<PositionProjection>()
                {
                   

                });
        }

        public PositionController PositionControllerFactory()
        {
            return new PositionController(QueryProcessor, CommandDispatcher);
        }

        //public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        //{
        //    CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
        //        .Callback<ICommand<PermissionDTO>>((a) => { CreatePermissionCommand = a; })
        //        .ReturnsAsync(new PermissionDTO());
        //}
    }
}
