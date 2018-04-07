//using UrbanSpork.Common.DataTransferObjects.Department;
//using UrbanSpork.DataAccess.Projections;
//using UrbanSpork.Tests.Controllers.Department.Mocks;
//using UrbanSpork.WriteModel.Commands;
//using Xunit;

//namespace UrbanSpork.Tests.Controllers.Department
//{
//    public class DepartmentControllerTests
//    {
//        [Fact]
//        public async void given_create_department_command_command_dispatcher_should_get_same_command_created_in_controller()
//        {
//            //Assemble
//            var mockAgg = new DepartmentControllerMockAggregate();

//            var input = new CreateDepartmentDTO()
//            {
//                Name = "testName"
//            };

//            mockAgg.setup_dispatcher_to_test_create_department_command(input);

//            var command = new CreateDepartmentCommand(input);

//            //Apply
//            var result = await mockAgg.CommandDispatcher.Execute(command);

//            //Assert
//            Assert.Equal(command, mockAgg.CreatePermissionCommand);
//            Assert.IsType<DepartmentProjection>(result);
//        }
//    }
//}