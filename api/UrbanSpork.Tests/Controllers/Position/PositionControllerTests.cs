using System;
using System.Collections.Generic;
using UrbanSpork.Common.DataTransferObjects.Position;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;
using Xunit;

namespace UrbanSpork.Tests.Controllers.Position
{
    public class PositionControllerTests
    {
        [Fact]
        public async void given_create_position_command_command_dispatcher_should_get_same_command_created_in_controller()
        {
            var mockAgg = new PositionControllerMockAggregate();

            var controller = mockAgg.PositionControllerFactory();

            var input = new CreatePositionDTO
            {
                PositionName = "testName",
            };

            var command = new CreatePositionCommand(input);

            mockAgg.setup_dispatcher_to_verify_createPositionCommands_are_the_same(command);

            var result = await controller.CreatePosition(input);

            //Assert
            Assert.IsType<PositionProjection>(result);
            Assert.Equal(result.PositionName, input.PositionName);
        }

        [Fact]
        public async void given_remove_position_command_command_dispatcher_should_get_same_command_created_in_controller()
        {
            var mockAgg = new PositionControllerMockAggregate();

            var controller = mockAgg.PositionControllerFactory();

            var input = new Guid();

            var command = new RemovePositionCommand(input);

            mockAgg.setup_dispatcher_to_verify_removePositionCommands_are_the_same(input);

            var result = await controller.RemovePosition(input);

            //Assert
            Assert.IsType<PositionProjection>(result);
            Assert.Equal(result.Id, input);
        }

        [Fact]
        public async void given_get_position_query_query_processor_should_get_same_query_created_in_controller()
        {
            var mockAgg = new PositionControllerMockAggregate();

            var controller = mockAgg.PositionControllerFactory();

            var command = new GetAllPositionsQuery();

            mockAgg.setup_processor_to_verify_getPositionQuery_are_the_same();

            var result = await controller.GetAllPositions();

            //Assert
            Assert.IsType<List<PositionProjection>> (result);
            
        }

        [Fact]
        public async void given_get_position_by_department_query_query_processor_should_get_same_query_created_in_controller()
        {
            var mockAgg = new PositionControllerMockAggregate();

            var controller = mockAgg.PositionControllerFactory();

            mockAgg.setup_processor_to_verify_getPositionByDepartmentQuery_are_the_same();

            var input = "test";
           

            var result = await controller.GetPositionsByDepartment(input);

            //Assert
            Assert.IsType<List<PositionProjection>>(result);

        }
    }
}