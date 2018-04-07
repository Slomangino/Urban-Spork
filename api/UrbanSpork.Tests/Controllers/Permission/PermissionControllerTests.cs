using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.WriteModel.Commands;
using Xunit;

namespace UrbanSpork.Tests.Controllers.Permission
{
    public class PermissionControllerTests
    {
        [Fact]
        public async void given_create_permission_command_command_dispatcher_should_get_same_command_created_in_controller()
        {
            //Assemble
            var mockAgg = new PermissionControllerMockAggregate();
            mockAgg.setup_dispatcher_to_verify_createPermissionCommands_are_the_same();

            var input = new CreateNewPermissionDTO
            {
                Name = "testName",
                Description = "testDesc",
                IsActive = true,
                Image = "testImage"
            };
            var command = new CreatePermissionCommand(input);

            //Apply
            var result = await mockAgg.CommandDispatcher.Execute(command);

            //Assert
            Assert.Equal(command, mockAgg.PermissionCommand);
            Assert.IsType<PermissionDTO>(result);
            Assert.Equal(result.Name, input.Name);
        }

        [Fact]
        public async void given_valid_input_create_permission_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var input = new CreateNewPermissionDTO
            {
                Name = "testName",
                Description = "testDesc",
                IsActive = true,
                Image = "testImage"
            };

            var command = new CreatePermissionCommand(input);

            mockAgg.setup_dispatcher_to_verify_createPermissionCommands_are_the_same();

            var result = await controller.CreatePermission(input);

            //Assert
            Assert.IsType<PermissionDTO>(result);
            Assert.Equal(result.Name, input.Name);
        }

        [Fact]
        public async void given_valid_input_update_permission_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var input = new UpdatePermissionInfoDTO()
            {
                Name = "testName",
                Description = "testDesc",
                Image = "testImage",

            };

            var command = new UpdatePermissionInfoCommand(input);

            mockAgg.setup_dispatcher_to_verify_updatePermissionCommands_are_the_same();

            var result = await controller.UpdatePermission(input);

            //Assert
            //Assert.Equal(command, mockAgg.PermissionCommand);
            Assert.IsType<PermissionDTO>(result);
            Assert.Equal(result.Name, input.Name);
        }
    }
}
