using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.WriteModel.CommandHandlers.PermissionActions;

namespace UrbanSpork.Tests.PermissionActions
{
    public class DenyUserPermissionRequestCommandHandlerMockAggregate
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

        public DenyUserPermissionRequestCommandHandler DenyUserPermissionRequestHandlerFactory()
        {
            return new DenyUserPermissionRequestCommandHandler(Email, Mapper, Session);
        }

        public UserPermissionsRequestedCommandHandler UserPermissionsRequestedHandlerFactory()
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

        public void setup_session_to_return_correct_aggregate(UserAggregate agg, PermissionAggregate permAgg)
        {
            // Get UserAggregate
            SessionMock.Setup(a => a.Get<UserAggregate>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .Returns(Task.FromResult(agg));

            // Get PermissionAggregate
            SessionMock.Setup(a => a.Get<PermissionAggregate>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetPermisisonWasCalled = true;
                })
                .Returns(Task.FromResult(permAgg));

            // Commit
            SessionMock.Setup(a => a.Commit(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionCommitWasCalled = true;
                })
                .Returns(Task.FromResult(0));
        }
    }
}
