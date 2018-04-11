using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.WriteModel.Commands.Permission;
using Xunit;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class CreatePermissionCommandHandlerTests
    {
        [Fact]
        public void given_CreatePermissionCommand_handler_should_call_session_Get_and_Commit()
        {
            var mockAgg = new CreatePermissionCommandHandlerMockAggregate();
            mockAgg.setup_session_to_return_correct_aggregate();
            var handler = mockAgg.CreatePermissionHandlerFactory();

            var input = new CreateNewPermissionDTO
            {
                Name = "testPermission",
                Description = "testDescription",
                IsActive = true,
                Image = "testUrl"
            };
            var command = new CreatePermissionCommand(input);

            // Apply 
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionAddWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
    }
}
