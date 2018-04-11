using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands.Permission;
using Xunit;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class DisablePermissionCommandHandlerTests
    {
        [Fact]
        public async void given_DisablePermissionCommand_handler_should_call_session_Get_and_Commit_on_active_permission()
        {
            // Assemble
            var mockAgg = new DisablePermissionCommandHandlerMockAggregate();
            var handler = mockAgg.DisablePermissionHandlerFactory();
            var testAgg = mockAgg.SetupAdminUser();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            mockAgg.setup_session_to_ensure_addAndCommit_are_called(testPermissionAgg, testAgg);
            mockAgg.setup_context_to_return_no_items();

            var input = new DisablePermissionInputDTO
            {
                PermissionId = testPermissionAgg.Id,
                ById = testAgg.Id,
            };

            var command = new DisablePermissionCommand(input);

            // Apply
            var result = await handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
    }
}
