using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.User;
using Xunit;

namespace UrbanSpork.Tests.User.CommandHandlerTests
{
    public class EnableSingleUserCommandHandlerTests
    {
        [Fact]
        public void given_EnableSingleUserCommand_handler_should_call_session_Get_and_commit_on_inactive_user()
        {
            // Assemble
            var mockaAgg = new EnableSingleUserCommandHandlerMockAggregate();
            mockaAgg.setup_session_to_ensure_GetAndCommit_are_called();
            var adminAgg = mockaAgg.SetupAdminUser();
            var testAgg = mockaAgg.SetupDisabledUser();

            var handler = mockaAgg.HandlerFactory();

            var input = new EnableUserInputDTO
            {
                UserId = testAgg.Id,
                ById = adminAgg.Id
            };
            var command = new EnableSingleUserCommand(input);

            // Apply 
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockaAgg.SessionGetWasCalled);
            Assert.True(mockaAgg.SessionCommitWasCalled);

        }
    }
}
