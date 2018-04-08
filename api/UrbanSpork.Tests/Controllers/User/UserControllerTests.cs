﻿using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.Tests.Controllers.Permission;
using UrbanSpork.WriteModel.Commands;
using Xunit;

namespace UrbanSpork.Tests.Controllers.User
{
    public class UserControllerTests
    {
        [Fact]
        public async void given_getuserbyid_query_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var id = new Guid();
            var query = new GetPermissionByIdQuery(id);

            mockAgg.setup_processor_to_verify_getUserQueries_are_the_same(id);

            var controller = mockAgg.CreateUserController();

            //Apply

            var result = await controller.Get(id);

            var projection = new UserDetailProjection()
            {
                FirstName = "Test",
                LastName = "Name",
                IsActive = true,
                IsAdmin = true, 
                Department = "Testeeee",
                Position = "Deper",
                Email ="asdasd@adad.com",
                DateCreated = new DateTime(),
                UserId = new Guid(),
                PermissionList = "Permissions Here",
            };

            var newProjection = new UserDetailProjection()
            {
                FirstName = projection.FirstName,
                LastName = projection.LastName,
                IsActive = projection.IsActive,
                IsAdmin = projection.IsAdmin,
                Department = projection.Department,
                Position = projection.Position,
                Email = projection.Email,
                DateCreated = projection.DateCreated,
                UserId = projection.UserId,
                PermissionList = projection.PermissionList
            };

            //await projection.ListenForEvents(new UserCreatedEvent
            //{
            //    FirstName = projection.FirstName,
            //    LastName = projection.LastName,
            //    IsActive = projection.IsActive,
            //    IsAdmin = projection.IsAdmin,
            //    Department = projection.Department,
            //    Position = projection.Position,
            //    Email = projection.Email,
            //    Id = projection.UserId,
            //    PermissionList = new Dictionary<Guid, PermissionDetails>()
            //});

            //await projection.ListenForEvents(new UserUpdatedEvent
            //{
            //    FirstName = projection.FirstName,
            //    LastName = projection.LastName,
            //    IsAdmin = projection.IsAdmin,
            //    Department = projection.Department,
            //    Position = projection.Position,
            //    Email = projection.Email,
            //    Id = projection.UserId,
            //});
            //await projection.ListenForEvents(new UserPermissionGrantedEvent());
            //await projection.ListenForEvents(new UserPermissionRequestDeniedEvent());
            //await projection.ListenForEvents(new UserPermissionRevokedEvent());
            //await projection.ListenForEvents(new UserDisabledEvent());
            //await projection.ListenForEvents(new UserEnabledEvent());

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<UserDetailProjectionDTO>(result);
            Assert.Equal(result.UserId, id);
        }

        [Fact]
        public async void given_get_user_collection_query_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            mockAgg.setup_processor_to_verify_getUserCollectionQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            var filter = new UserFilterCriteria();

            //Apply
            var result = await controller.GetUserCollection(filter);

            //Assert
            Assert.IsType<List<UserDetailProjectionDTO>>(result);
        }

        [Fact]
        public async void given_getoffboarduserpermissionsquery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var id = new Guid();
            var query = new GetOffboardUserPermissionsQuery(id);

            mockAgg.setup_processor_to_verify_getUserOffboardingQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply

            var result = await controller.GetOffBoardingPermissionsById(id);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<OffBoardUserDTO>(result);
        }

        [Fact]
        public async void given_GetApproverActicityProjectionQuery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var input = new ApproverActivityFilterCriteria();
            var query = new GetApproverActicityProjectionQuery
            {
                FilterCriteria = input
            };

            mockAgg.setup_processor_to_verify_getApproverActivityQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply

            var result = await controller.GetApproverAtivityProjection(input);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<List<ApproverActivityProjection>>(result);
        }

        [Fact]
        public async void given_GetUserManagementProjectionQuery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var input = new UserFilterCriteria();

            mockAgg.setup_processor_to_verify_getUserListQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply
            var result = await controller.GetUserList(input);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<List<UserManagementDTO>>(result);
        }

        [Fact]
        public async void given_GetSystemActivityReportQuery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var input = new SystemActivityReportFilterCriteria();

            mockAgg.setup_processor_to_verify_getSystemActivityReportQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply
            var result = await controller.GetSystemActivity(input);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<List<SystemActivityDTO>>(result);
        }

        [Fact]
        public async void given_GetSystemReportQuery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var input = new SystemReportFilterCriteria();

            mockAgg.setup_processor_to_verify_getSystemReportQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply
            var result = await controller.GetSystemReport(input);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<List<SystemActivityDTO>>(result);
        }

        [Fact]
        public async void given_GetSystemDashboardQuery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            var input = new DashboardFilterCriteria();

            mockAgg.setup_processor_to_verify_getSystemDashboardQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply
            var result = await controller.GetSystemDashboard(input);

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<List<DashboardProjection>>(result);
        }

        [Fact]
        public async void given_GetLoginUsersQuery_queryprocessor_should_get_same_query_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();

            mockAgg.setup_processor_to_verify_getLoginUsersQueries_are_the_same();

            var controller = mockAgg.CreateUserController();

            //Apply
            var result = await controller.GetLoginUsers();

            //Assert
            //Assert.Equal(query, mockAgg.PermissionQuery);
            Assert.IsType<List<LoginUserDTO>>(result);
        }

        [Fact]
        public async void given_create_permission_command_command_dispatcher_should_get_same_command_created_in_controller()
        {
            //Assemble
            var mockAgg = new UserControllerMockAggregate();
            

            var input = new CreateUserInputDTO()
            {
                FirstName = "testFName",
                LastName = "testLName"
            };

            var command = new CreateSingleUserCommand(input);

            mockAgg.setup_dispatcher_to_verify_createUserCommands_are_the_same(command);

            var controller = mockAgg.CreateUserController();

            //Apply
            var result = await controller.CreateUser(input);

            //Assert
            Assert.IsType<UserDTO>(result);
            Assert.Equal(result.FirstName, input.FirstName);
            Assert.Equal(result.LastName, input.LastName);
        }
    }
}
