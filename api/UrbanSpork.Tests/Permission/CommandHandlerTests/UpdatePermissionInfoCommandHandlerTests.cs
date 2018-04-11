using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.WriteModel.Commands.Permission;
using Xunit;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class UpdatePermissionInfoCommandHandlerTests
    {
        [Fact]
        public void given_UpdatePermissionInfoCommand_handler_should_call_session_Get_and_Commit()
        {
            // Asemble
            var mockAgg = new UpdatePermissionInfoCommandHandlerMockAggregate();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            var testAgg = mockAgg.SetupAdminUser();
            mockAgg.setup_session_to_ensure_addAndCommit_are_called(testPermissionAgg);
            var handler = mockAgg.UpdatePermissionInfoHandlerFactory();
            var input = new UpdatePermissionInfoDTO
            {
                Id = testPermissionAgg.Id,
                UpdatedById = testAgg.Id,
                Name = "changedName",
                Description = "changedDescription",
                Image = "changedImageUrl"
            };

            var command = new UpdatePermissionInfoCommand(input);

            // Apply
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
        // Asemble
        // Apply
        // Assert
    }
}
