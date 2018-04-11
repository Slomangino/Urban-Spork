using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.WriteModel.Commands.Permission;
using Xunit;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class EnablePermissionCommandHandlerTests
    {
        [Fact]
        public void given_UpdatePermissionInfoCommand_handler_should_call_session_Get_and_Commit()
        {
            // Asemble
            var mockAgg = new EnablePermissionCommandHandlerMockAggregate();
            var testPermissionAgg = mockAgg.SetupTestDisabledPermission();
            var testAgg = mockAgg.SetupAdminUser();
            mockAgg.setup_session_to_ensure_addAndCommit_are_called(testPermissionAgg, testAgg);
            var handler = mockAgg.EnablePermissionHandlerFactory();
            var input = new EnablePermissionInputDTO()
            {
                ById = testAgg.Id,
                PermissionId = testPermissionAgg.Id
            };

            var command = new EnablePermissionCommand(input);

            // Apply
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionGetPermissionWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
        // Asemble
        // Apply
        // Assert
    }
}
