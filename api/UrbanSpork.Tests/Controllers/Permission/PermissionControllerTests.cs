using System;
using System.Collections.Generic;
using System.Text;
using Castle.Components.DictionaryAdapter;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;
using Xunit;

namespace UrbanSpork.Tests.Controllers.Permission
{
    public class PermissionControllerTests
    {
        [Fact]
        public async void given_get_permission_query_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new PermissionControllerMockAggregate();

            var id = new Guid();
            var query = new GetPermissionByIdQuery(id);
            
            mockAgg.setup_processor_to_verify_getPermissionQueries_are_the_same(id);

            var controller = mockAgg.PermissionControllerFactory();

            //Apply

            var result = await controller.Get(id);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<PermissionDTO>(result);
            Assert.Equal(result.Id, id);
        }

        [Fact]
        public async void given_get_all_permissions_query_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new PermissionControllerMockAggregate();

            mockAgg.setup_processor_to_verify_getAllPermissionQueries_are_the_same();


            var controller = mockAgg.PermissionControllerFactory();

            //Apply
            var result = await controller.GetAllPermissions();

            //Assert
            Assert.IsType<List<PermissionDTO>>(result);
        }

        [Fact]
        public async void given_get_System_dropdown_projection_query_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new PermissionControllerMockAggregate();

            mockAgg.setup_processor_to_verify_getSystemDropdownProjectionQueries_are_the_same();

            var controller = mockAgg.PermissionControllerFactory();

            //Apply
            var result = await controller.GetSystemDropDownProjection();

            //Assert
            Assert.IsType<List<SystemDropdownProjection>>(result);
        }

        [Fact]
        public async void given_get_pending_requests_query_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new PermissionControllerMockAggregate();

            mockAgg.setup_processor_to_verify_getPendingRequestsQueries_are_the_same();

            var controller = mockAgg.PermissionControllerFactory();

            //Apply
            var result = await controller.GetPendingRequests();


            //List<PendingRequestsProjection>
            //Assert
            Assert.IsType<List<PendingRequestsProjection>>(result);
        }

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

        [Fact]
        public async void given_valid_input_disable_permission_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var permissionId = new Guid();

            var byId = new Guid();

            var input = new DisablePermissionInputDTO()
            {
                PermissionId = permissionId,
                ById = byId
            };

            var command = new DisablePermissionCommand(input);

            mockAgg.setup_dispatcher_to_verify_disablePermissionCommands_are_the_same(permissionId);

            var result = await controller.DisablePermission(input);

            //Assert
            Assert.IsType<PermissionDTO>(result);
            Assert.Equal(result.Id, input.PermissionId);
        }

        [Fact]
        public async void given_valid_input_enable_permission_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var permissionId = new Guid();

            var byId = new Guid();

            var input = new EnablePermissionInputDTO()
            {
                PermissionId = permissionId,
                ById = byId
            };

            var command = new EnablePermissionCommand(input);

            mockAgg.setup_dispatcher_to_verify_enablePermissionCommands_are_the_same(permissionId);

            var result = await controller.EnablePermission(input);

            //Assert
            Assert.IsType<PermissionDTO>(result);
            Assert.Equal(result.Id, input.PermissionId);
        }

        [Fact]
        public async void given_valid_input_create_permission_template_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var templateName = "TestName";

            var permissions = new Dictionary<Guid, string>();

            permissions.Add(new Guid(), "TestInput");

            var input = new CreatePermissionTemplateInputDTO()
            {
                Name = templateName,
                TemplatePermissions = permissions
            };

            var command = new CreatePermissionTemplateCommand(input);

            mockAgg.setup_dispatcher_to_verify_createPermissionTemplateCommands_are_the_same(input);

            var result = await controller.CreatePermissionTemplate(input);

            //Assert
            Assert.IsType<PermissionTemplateDTO>(result);
            Assert.Equal(result.Name, input.Name);
            Assert.Equal(result.TemplatePermissions, result.TemplatePermissions);
        }

        [Fact]
        public async void given_valid_input_edit_permission_template_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var templateName = "TestName";

            var permissions = new Dictionary<Guid, string>();

            permissions.Add(new Guid(), "TestInput");

            var input = new EditPermissionTemplateInputDTO()
            {
                Name = templateName,
                TemplatePermissions = permissions
            };

            var command = new EditPermissionTemplateCommand(input);

            mockAgg.setup_dispatcher_to_verify_editPermissionTemplateCommands_are_the_same(input);

            var result = await controller.EditPermissionTemplate(input);

            //Assert
            Assert.IsType<PermissionTemplateDTO>(result);
            Assert.Equal(result.Name, input.Name);
            Assert.Equal(result.TemplatePermissions, result.TemplatePermissions);
        }

        [Fact]
        public async void given_valid_input_delete_permission_template_method_returns_valid_results()
        {
            var mockAgg = new PermissionControllerMockAggregate();

            var controller = mockAgg.PermissionControllerFactory();

            var templateName = "TestName";

            var id = new Guid();

            var input = new DeletePermissionTemplateInputDTO()
            {
                Id = id
            };

            var command = new DeletePermissionTemplateCommand(input);

            mockAgg.setup_dispatcher_to_verify_deletePermissionTemplateCommands_are_the_same(input);

            var result = await controller.DeletePermissionTemplate(input);

            //Assert
            Assert.IsType<string>(result);
        }
    }
}
