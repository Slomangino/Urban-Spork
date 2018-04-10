using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.PermissionActions;
using Xunit;

namespace UrbanSpork.Tests.Permission
{
    public class UserPermissionsRequestedCommandHandlerTests
    {
        [Fact]
        public void given_UserPermissionsRequestedCommand_handler_should_call_session_Get_and_Commit_for_unlisted_permission()
        {
            // Assemble
            var mockAgg = new UserPermissionsRequestedCommandHandlerMockAggregate();
            mockAgg.setup_session_to_ensure_addAndCommit_are_called();
            var testAgg = mockAgg.SetupAdminUser();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            var handler = mockAgg.HandlerFactory();

            var input = new RequestUserPermissionsDTO
            {
                ForId = testAgg.Id,
                ById = testAgg.Id,
                Requests = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        testPermissionAgg.Id, new PermissionDetails
                        {
                            Reason = "testRequestReason"
                        }
                    }
                }
            };

            var command = new UserPermissionsRequestedCommand(input);

            // Apply
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }

        //this test doesnt actually do what it says it is supposed to do
        [Fact]
        public async void handler_should_filter_already_requested_command_out_of_requests()
        {
            // Assemble
            var mockAgg = new UserPermissionsRequestedCommandHandlerMockAggregate();
            mockAgg.setup_session_to_ensure_addAndCommit_are_called();
            var testAgg = mockAgg.SetupAdminUser();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            var handler = mockAgg.HandlerFactory();

            var input = new RequestUserPermissionsDTO
            {
                ForId = testAgg.Id,
                ById = testAgg.Id,
                Requests = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        testPermissionAgg.Id, new PermissionDetails
                        {
                            Reason = "testRequestReason"
                        }
                    }
                }
            };

            var command = new UserPermissionsRequestedCommand(input);

            // Apply
            var result = await handler.Handle(command);
            
            Assert.True(mockAgg.SessionCommitWasCalled);

            mockAgg.setup_session_to_return_aggregate_with_requested_permission(testAgg, testPermissionAgg);
            
            var result2 = await handler.Handle(command);

            // Assert
            Assert.False(mockAgg.SessionCommitWasCalled);
        }
    }
}
