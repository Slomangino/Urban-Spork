using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.PermissionActions;
using Xunit;

namespace UrbanSpork.Tests.PermissionActions
{
    public class RevokeUserPermissionCommandHandlerTests
    {
        [Fact]
        public void give_RevokeUserPermissionCommand_handler_should_call_session_Get_and_Commit_on_granted_permission()
        {
            // Assemble
            var mockAgg = new RevokeUserPermissionCommandHandlerMockAggregate();
            var testAgg = mockAgg.SetupAdminUser();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            mockAgg.setup_session_to_return_correct_aggregate(testAgg, testPermissionAgg);
            var grantHandler = mockAgg.GrantUserPermissionRequestHandlerFactory();
            var revokeHandler = mockAgg.RevokeUserPermissionHandlerFactory();

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

            var revokeInput = new RevokeUserPermissionDTO
            {
                ForId = testAgg.Id,
                ById = testAgg.Id,
                PermissionsToRevoke = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        testPermissionAgg.Id, new PermissionDetails
                        {
                            Reason = "testRevokeReason"
                        }
                    }
                }
            };

            var revokeCommand = new RevokeUserPermissionCommand(revokeInput);

            var grantResult = grantHandler.Handle(grantCommand);

            // Apply
            var revokeResult = revokeHandler.Handle(revokeCommand);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
            Assert.True(mockAgg.SessionGetPermisisonWasCalled);
        }
    }
}
