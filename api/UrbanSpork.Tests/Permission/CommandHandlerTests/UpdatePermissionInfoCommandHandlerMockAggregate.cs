using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.CommandHandlers.Permission;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class UpdatePermissionInfoCommandHandlerMockAggregate
    {
        public static readonly Mock<ISession> SessionMock = new Mock<ISession>();
        public static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();


        public readonly ISession Session = SessionMock.Object;
        public readonly IMapper Mapper = MapperMock.Object;

        public bool SessionGetWasCalled = false;
        public bool SessionCommitWasCalled = false;

        public UpdatePermissionInfoCommandHandler UpdatePermissionInfoHandlerFactory()
        {
            return new UpdatePermissionInfoCommandHandler(Mapper, Session);
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

        public async void setup_session_to_ensure_addAndCommit_are_called(PermissionAggregate permAagg)
        {
            SessionMock.Setup(a => a.Get<PermissionAggregate>(permAagg.Id, It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .ReturnsAsync(await Task.FromResult(permAagg));

          SessionMock.Setup(a => a.Commit(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionCommitWasCalled = true;
                })
                .Returns(Task.FromResult(0));
        }
    }
}
