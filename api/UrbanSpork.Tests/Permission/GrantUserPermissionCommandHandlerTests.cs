using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.PermissionActions;
using Xunit;

namespace UrbanSpork.Tests.Permission
{
    public class GrantUserPermissionCommandHandlerTests
    {
        [Fact]
        public void given_GrantUserPermissionCommand_handler_should_call_session_Get_and_Commit_on_requested_permission()
        {
            // Assemble
            var mockAgg = new GrantUserPermissionCommandHandlerMockAggregate();
            var requestHandler = mockAgg.UserPermissionsRequestedHandlerFactory();
            var granthandler = mockAgg.GrantUserPermissionHandlerFactory();
            var testAgg = mockAgg.SetupAdminUser();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            mockAgg.setup_session_to_return_correct_aggregate(testAgg, testPermissionAgg);

            var grantInput = new GrantUserPermissionDTO
            {
                ForId = testAgg.Id,
                ById = testAgg.Id,
                PermissionsToGrant = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        testPermissionAgg.Id, new PermissionDetails
                        {
                            Reason = "testGrantReason"
                        }
                    }
                }
            };

            var grantCommand = new GrantUserPermissionCommand(grantInput);

            var requestInput = new RequestUserPermissionsDTO
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

            var requestCommand = new UserPermissionsRequestedCommand(requestInput);

            var requestResult = requestHandler.Handle(requestCommand);

            // Apply
            var grantResult = granthandler.Handle(grantCommand);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
            Assert.True(mockAgg.SessionGetPermisisonWasCalled);
        }
    }
}
