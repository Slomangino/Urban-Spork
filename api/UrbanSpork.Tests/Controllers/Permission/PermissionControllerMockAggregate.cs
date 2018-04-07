using Moq;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.Tests.Controllers.Permission
{
    public class PermissionControllerMockAggregate
    {
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public static readonly Mock<IQueryProcessor> QueryProcessorMock = new Mock<IQueryProcessor>();

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        private readonly IQueryProcessor QueryProcessor = QueryProcessorMock.Object;

        public ICommand<PermissionDTO> PermissionCommand;

        public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => { PermissionCommand = a; })
                .ReturnsAsync(new PermissionDTO()
                {
                    Name = "testName",
                    Description = "testDesc",
                    IsActive = true,
                    Image = "testImage"
                });
        }

        public void setup_dispatcher_to_verify_updatePermissionCommands_are_the_same()
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<UpdatePermissionInfoCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => { PermissionCommand = a; })
                .ReturnsAsync(new PermissionDTO()
                {
                    Name = "testName",
                    Description = "testDesc",
                    Image = "testImage"
                });
        }

        public PermissionController PermissionControllerFactory()
        {
            return new PermissionController(QueryProcessor, CommandDispatcher);
        }

        //public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        //{
        //    CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
        //        .Callback<ICommand<PermissionDTO>>((a) => { CreatePermissionCommand = a; })
        //        .ReturnsAsync(new PermissionDTO());
        //}
    }
}
