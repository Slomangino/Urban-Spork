using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.WriteModel.Commands.User;
using Xunit;

namespace UrbanSpork.Tests.User.CommandHandlerTests
{
    public class UpdateSingleUserCommandHandlerTests
    {
        [Fact]
        public void given_UpdateSingleUserCommand_handler_should_call_session_get_and_commit()
        {
            // Assemble
            var mockAgg = new UpdateSingleUserCommandHandlerMockAggregate();
            mockAgg.setup_session_to_ensure_GetAndCommit_are_called();
            var handler = mockAgg.HandlerFactory();
            var testAgg = mockAgg.SetupAdminUser();

            var input = new UpdateUserInformationDTO
            {
                ForID = testAgg.Id,
                FirstName = "changed",
                LastName = "changed",
                Email = "changed",
                Position = "changed",
                Department = "changed",
                IsAdmin = false
            };
            var command = new UpdateSingleUserCommand(input.ForID, input);

            // Apply
            var result = handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionGetWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
    }
}
// Assemble
// Apply
// Assert