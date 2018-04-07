using System;
using System.Collections.Generic;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.Tests.Controllers.Department;
using UrbanSpork.WriteModel.Commands;
using Xunit;

namespace UrbanSpork.Tests.Controllers.Department
{
    public class DepartmentControllerTests
    {
        [Fact]
        public async void given_create_department_command_command_dispatcher_should_get_same_command_created_in_controller()
        {
            var mockAgg = new DepartmentControllerMockAggregate();

            var controller = mockAgg.DepartmentControllerFactory();

            var input = new CreateDepartmentDTO
            {
                Name = "testName",
            };

            var command = new CreateDepartmentCommand(input);

            mockAgg.setup_dispatcher_to_verify_createDepartmentCommands_are_the_same();

            var result = await controller.CreateDepartment(input);

            //Assert
            Assert.IsType<DepartmentProjection>(result);
            Assert.Equal(result.Name, input.Name);
        }

        [Fact]
        public async void given_remove_department_command_command_dispatcher_should_get_same_command_created_in_controller()
        {
            var mockAgg = new DepartmentControllerMockAggregate();

            var controller = mockAgg.DepartmentControllerFactory();

            var input = new Guid();

            var command = new RemoveDepartmentCommand(input);

            mockAgg.setup_dispatcher_to_verify_removeDepartmentCommands_are_the_same(input);

            var result = await controller.RemoveDepartment(input);

            //Assert
            Assert.IsType<DepartmentProjection>(result);
            Assert.Equal(result.Id, input);
        }

        [Fact]
        public async void given_get_department_query_query_processor_should_get_same_query_created_in_controller()
        {
            var mockAgg = new DepartmentControllerMockAggregate();

            var controller = mockAgg.DepartmentControllerFactory();

            var command = new GetDepartmentsQuery();

            mockAgg.setup_processor_to_verify_getDepartmentQuery_are_the_same();

            var result = await controller.GetAllDepartments();

            //Assert
            Assert.IsType<List<DepartmentProjection >> (result);
            
        }
    }
}