using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;
using Xunit;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests.TemplateTests
{
    public class DeletePermissionTemplateCommandHandlerTests
    {
        [Fact]
        public async void given_DeletePermissionTemplateCommand_handler_should_call_context_Remove_and_Save()
        {
            // Assemble
            var mockAgg = new DeletePermissionTemplateCommandHandlerMockAggregate();
            var testTemplate = mockAgg.SetupTestPermissionTemplate();
            mockAgg.setup_context_to_return_one_item(testTemplate);
            var handler = mockAgg.DeletePermissionTemplateHandlerFactory();

            //await mockAgg.Context.PermissionTemplateProjection.AddAsync(testTemplate);

            var input = new DeletePermissionTemplateInputDTO
            {
                Id = testTemplate.Id
            };

            var command = new DeletePermissionTemplateCommand(input);

            // Apply
            var result = await handler.Handle(command);

            // Assert
            Assert.True(mockAgg.ContextRemoveWasCalled);
            Assert.True(mockAgg.ContextSaveWasCalled);
        }
    }
}
