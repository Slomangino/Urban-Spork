using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.User;
using Xunit;

namespace UrbanSpork.Tests.User.CommandHandlerTests
{
    public class DisableSingleUserCommandHandlerTests
    {
        [Fact]
        public void given_DisableSingleUserCommand_handler_should_call_session_Get_and_Commit_on_active_user()
        {
            // Assemble
            var mockAgg = new DisableSingleUserCommandHandlerMockAggregate();
            mockAgg.setup_session_to_ensure_GetAndCommit_are_called();
            var testAgg = mockAgg.SetupAdminUser();
            var handler = mockAgg.HandlerFactory();

            var input = new DisableUserInputDTO
            {
                UserId = testAgg.Id,
                ById = testAgg.Id
            };

            var command = new DisableSingleUserCommand(input);

            // Apply
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
    }
}
