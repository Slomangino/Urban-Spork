using Moq;
using System;
using System.Collections.Generic;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.Commands.User;
using UrbanSpork.WriteModel.WriteModel.Commands.User;

namespace UrbanSpork.Tests.Controllers.User
{
    class UserControllerMockAggregate
    {
        #region Mocks
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public static readonly Mock<IQueryProcessor> QueryProcessorMock = new Mock<IQueryProcessor>();

        #endregion

        #region Objects

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        public readonly IQueryProcessor QueryProcessor = QueryProcessorMock.Object;

        #endregion

        #region Commands & Queries

        public IQuery<UserDetailProjectionDTO> UserQuery;

        public IQuery<List<UserDetailProjectionDTO>> UserCollectionQuery;

        public IQuery<OffBoardUserDTO> OffboardQuery;

        public IQuery<List<ApproverActivityProjection>> ApproverActivityQuery;

        public IQuery<List<UserManagementDTO>> UserManagementQuery;

        public IQuery<List<SystemActivityDTO>> SystemActivityQuery;

        public IQuery<List<DashboardProjection>> DashboardQuery;

        public IQuery<List<LoginUserDTO>> LoginUserQuery;

        public ICommand<UserDTO> UserCommand;

        public ICommand<UpdateUserInformationDTO> UpdateUserCommand;

        #endregion

        #region Factories

        public UserController CreateUserController()
        {
            return new UserController(QueryProcessor, CommandDispatcher);
        }

        #endregion

        #region Setup Methods

        public void setup_processor_to_verify_getUserQueries_are_the_same(Guid id)
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetUserDetailByIdQuery>()))
                .Callback<IQuery<UserDetailProjectionDTO>>((a) => { UserQuery = a; })
                .ReturnsAsync(new UserDetailProjectionDTO
                {
                    UserId = id
                });
        }

        public void setup_processor_to_verify_getUserCollectionQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetUserCollectionQuery>()))
                .Callback<IQuery<List<UserDetailProjectionDTO>>>((a) => { UserCollectionQuery = a; })
                .ReturnsAsync(new List<UserDetailProjectionDTO>());
        }

        public void setup_processor_to_verify_getUserOffboardingQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetOffboardUserPermissionsQuery>()))
                .Callback<IQuery<OffBoardUserDTO>>((a) => { OffboardQuery = a; })
                .ReturnsAsync(new OffBoardUserDTO());
        }

        public void setup_processor_to_verify_getApproverActivityQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetApproverActicityProjectionQuery>()))
                .Callback<IQuery<List<ApproverActivityProjection>>>((a) => { ApproverActivityQuery = a; })
                .ReturnsAsync(new List<ApproverActivityProjection>());
        }

        public void setup_processor_to_verify_getUserListQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetUserManagementProjectionQuery>()))
                .Callback<IQuery<List<UserManagementDTO>>>((a) => { UserManagementQuery = a; })
                .ReturnsAsync(new List<UserManagementDTO>());
        }

        public void setup_processor_to_verify_getSystemActivityReportQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetSystemActivityReportQuery>()))
                .Callback<IQuery<List<SystemActivityDTO>>>((a) => { SystemActivityQuery = a; })
                .ReturnsAsync(new List<SystemActivityDTO>());
        }

        public void setup_processor_to_verify_getSystemReportQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetSystemReportQuery>()))
                .Callback<IQuery<List<SystemActivityDTO>>>((a) => { SystemActivityQuery = a; })
                .ReturnsAsync(new List<SystemActivityDTO>());
        }

        public void setup_processor_to_verify_getSystemDashboardQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetSystemDashboardQuery>()))
                .Callback<IQuery<List<DashboardProjection>>>((a) => { DashboardQuery = a; })
                .ReturnsAsync(new List<DashboardProjection>());
        }

        public void setup_processor_to_verify_getLoginUsersQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetLoginUsersQuery>()))
                .Callback<IQuery<List<LoginUserDTO>>>((a) => { LoginUserQuery = a; })
                .ReturnsAsync(new List<LoginUserDTO>());
        }

        public void setup_dispatcher_to_verify_createUserCommands_are_the_same(CreateSingleUserCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreateSingleUserCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (CreateSingleUserCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    FirstName = command.Input.FirstName,
                    LastName = command.Input.LastName
                });
        }

        public void setup_dispatcher_to_verify_updateUserInformationCommands_are_the_same(UpdateSingleUserCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<UpdateSingleUserCommand>()))
                .Callback<ICommand<UpdateUserInformationDTO>>((a) => { UpdateUserCommand = (UpdateSingleUserCommand)a; })
                .ReturnsAsync(new UpdateUserInformationDTO()
                {
                    FirstName = command.Input.FirstName,
                    LastName = command.Input.LastName,
                    ForID = command.Input.ForID
                });
        }

        public void setup_dispatcher_to_verify_enableSingleUserCommands_are_the_same(EnableSingleUserCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<EnableSingleUserCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (EnableSingleUserCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    Id = command.Input.UserId
                });
        }

        public void setup_dispatcher_to_verify_disableSingleUserCommands_are_the_same(DisableSingleUserCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<DisableSingleUserCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (DisableSingleUserCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    Id = command.Input.UserId
                });
        }

        public void setup_dispatcher_to_verify_userPermissionsRequestedCommands_are_the_same(UserPermissionsRequestedCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<UserPermissionsRequestedCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (UserPermissionsRequestedCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    Id = command.Input.ForId,
                    PermissionList = command.Input.Requests
                });
        }

        public void setup_dispatcher_to_verify_denyUserPermissionRequestCommands_are_the_same(DenyUserPermissionRequestCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<DenyUserPermissionRequestCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (DenyUserPermissionRequestCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    Id = command.Input.ForId,
                    PermissionList = command.Input.PermissionsToDeny
                });
        }

        public void setup_dispatcher_to_verify_grantUserPermissionRequestCommands_are_the_same(GrantUserPermissionCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<GrantUserPermissionCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (GrantUserPermissionCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    Id = command.Input.ForId,
                    PermissionList = command.Input.PermissionsToGrant
                });
        }

        public void setup_dispatcher_to_verify_revokeUserPermissionRequestCommands_are_the_same(RevokeUserPermissionCommand command)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<RevokeUserPermissionCommand>()))
                .Callback<ICommand<UserDTO>>((a) => { UserCommand = (RevokeUserPermissionCommand)a; })
                .ReturnsAsync(new UserDTO()
                {
                    Id = command.Input.ForId,
                    PermissionList = command.Input.PermissionsToRevoke
                });
        }
        #endregion
    }
}
