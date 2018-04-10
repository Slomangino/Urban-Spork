using System;
using System.Collections.Generic;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.WriteModel.Commands.User;
using Xunit;

namespace UrbanSpork.Tests.User.CommandHandlerTests
{
    public class CreateSingleUserCommandHandlerTests
    {
        [Fact]
        public async void given_CreateSingleUserCommand_handler_should_have_correct_command_type()
        {
            // Assemble
            CreateUserInputDTO input = new CreateUserInputDTO
            {
                FirstName = "test",
                LastName = "testLastName",
                Email = "testEmail",
                Position = "testPosition",
                Department = "testDepartment",
                IsAdmin = true,
                IsActive = true,
                PermissionList = new Dictionary<Guid, PermissionDetails>()
            };
            var command = new CreateSingleUserCommand(input);
            var mockAgg = new CreateSingleUserCommandHandlerMockAggregate();
            mockAgg.setup_session_to_ensure_addAndCommit_are_called();
            var handler = mockAgg.HandlerFactory();

            // Apply
            var result = await handler.Handle(command);

            // Assert
            Assert.True(mockAgg.SessionAddWasCalled);
            Assert.True(mockAgg.SessionCommitWasCalled);
        }
    }
}
// Assemble
// Apply
// Assert