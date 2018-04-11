using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;
using Xunit;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests.TemplateTests
{
    public class CreatePermissionTemplateCommandHandlerTests
    {
        [Fact]
        public async void given_CreatePermissionTemplateCommand_handler_should_call_context_Add_and_Save()
        {
            // Assemble
            var mockAgg = new CreatePermissionTemplateCommandHandlerMockAggregate();
            var testPermissionAgg = mockAgg.SetupTestPermission();
            mockAgg.setup_context_to_return_one_item(testPermissionAgg);
            var handler = mockAgg.CreatePermissionTemplateHandlerFactory();

            var input = new CreatePermissionTemplateInputDTO
            {
                Name = "testTemplateName",
                TemplatePermissions =  new Dictionary<Guid, string>
                                        {
                                            {testPermissionAgg.Id, testPermissionAgg.Name}
                                        }
            };

            var command = new CreatePermissionTemplateCommand(input);

            // Apply
            var result = await handler.Handle(command);

            // Assert
            Assert.True(mockAgg.ContextSaveWasCalled);
            Assert.True(mockAgg.ContextAddWasCalled);
        }
    }
}
