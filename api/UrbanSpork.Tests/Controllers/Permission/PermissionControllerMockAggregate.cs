using Moq;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.Tests.Controllers.Permission
{
    public class PermissionControllerMockAggregate
    {
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        public ICommand<PermissionDTO> CreatePermissionCommand;

        public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => { CreatePermissionCommand = a; })
                .ReturnsAsync(new PermissionDTO());
        }
    }
}
