using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.PermissionActions;
using Xunit;

namespace UrbanSpork.Tests.PermissionActions
{
    public class DenyUserPermissionRequestCommandHandlerTests
    {
        [Fact]
        public async void given_DenyUserPermissionRequestCommand_handler_should_call_session_Get_and_Commit_when_permission_was_requested()
        {
            // Assemble
            var mockAgg = new DenyUserPermissionRequestCommandHandlerMockAggregate();
            var testAgg = mockAgg.SetupAdminUser();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            var denyHandler = mockAgg.DenyUserPermissionRequestHandlerFactory();
            var requestHandler = mockAgg.UserPermissionsRequestedHandlerFactory();
            mockAgg.setup_session_to_return_correct_aggregate(testAgg, testPermissionAgg);

            var denyInput = new DenyUserPermissionRequestDTO
            {
                ForId = testAgg.Id,
                ById = testAgg.Id,
                PermissionsToDeny = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        testPermissionAgg.Id, new PermissionDetails
                        {
                            Reason = "testDenyReason"
                        }
                    }
                }
            };

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
            var result = await requestHandler.Handle(requestCommand);

            var denyCommand = new DenyUserPermissionRequestCommand(denyInput);
            // Apply
            var denyResult = await denyHandler.Handle(denyCommand);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
            Assert.True(mockAgg.SessionGetPermisisonWasCalled);
        }
    }
}
