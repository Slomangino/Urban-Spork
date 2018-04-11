using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.WriteModel.CommandHandlers.PermissionActions;
using UrbanSpork.WriteModel.CommandHandlers.User;

namespace UrbanSpork.Tests.PermissionActions
{
    public class UserPermissionsRequestedCommandHandlerMockAggregate
    {
        public static readonly Mock<ISession> SessionMock = new Mock<ISession>();
        public static readonly Mock<IEmail> EmailMock = new Mock<IEmail>();
        public static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();

        public readonly ISession Session = SessionMock.Object;
        public readonly IEmail Email = EmailMock.Object;
        public readonly IMapper Mapper = MapperMock.Object;

        public bool SessionGetWasCalled = false;
        public bool SessionCommitWasCalled = false;
        public bool SessionGetPermisisonWasCalled = false;

        public UserPermissionsRequestedCommandHandler HandlerFactory()
        {
            return new UserPermissionsRequestedCommandHandler(Email, Mapper, Session);
        }

        public UserAggregate SetupAdminUser()
        {

            var dto = new CreateUserInputDTO
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

            var agg = UserAggregate.CreateNewUser(dto);
            return agg;
        }

        public PermissionAggregate SetupTestPermission()
        {
            var createPermisisonDTO = new CreateNewPermissionDTO
            {
                Name = "testPermisison",
                Description = "testDescription",
                IsActive = true,
                Image = "testUrl"
            };

            var permAgg = PermissionAggregate.CreateNewPermission(createPermisisonDTO);
            return permAgg;
        }

        public void setup_session_to_ensure_addAndCommit_are_called()
        {
            var agg = SetupAdminUser();
            SessionMock.Setup(a => a.Get<UserAggregate>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .Returns(Task.FromResult(agg));

            var permAgg = SetupTestPermission();
            SessionMock.Setup(a => a.Get<PermissionAggregate>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetPermisisonWasCalled = true;
                })
                .Returns(Task.FromResult(permAgg));
            
            SessionMock.Setup(a => a.Commit(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionCommitWasCalled = true;
                })
                .Returns(Task.FromResult(0));
        }

        public void setup_session_to_return_aggregate_with_requested_permission(UserAggregate agg, PermissionAggregate permAgg)
        {
            SessionCommitWasCalled = false;

            var input = new RequestUserPermissionsDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                Requests = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permAgg.Id, new PermissionDetails
                        {
                            Reason = "testReason"
                        }
                    }
                }
            };

            agg.UserRequestedPermissions(new List<PermissionAggregate>{permAgg}, input);

            SessionMock.Setup(a => a.Get<UserAggregate>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .Returns(Task.FromResult(agg));
        }
    }
}
