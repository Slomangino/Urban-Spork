using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.WriteModel.CommandHandlers.User;

namespace UrbanSpork.Tests.User.CommandHandlerTests
{
    public class DisableSingleUserCommandHandlerMockAggregate
    {
        public static readonly Mock<ISession> SessionMock = new Mock<ISession>();
        public static readonly Mock<IEmail> EmailMock = new Mock<IEmail>();
        public static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();

        public readonly ISession Session = SessionMock.Object;
        public readonly IEmail Email = EmailMock.Object;
        public readonly IMapper Mapper = MapperMock.Object;

        public bool SessionGetWasCalled = false;
        public bool SessionCommitWasCalled = false;

        public DisableSingleUserCommandHandler HandlerFactory()
        {
            return new DisableSingleUserCommandHandler(Session, Mapper);
        }

        public UserAggregate SetupAdminUser()
        {
            var dto = new CreateUserInputDTO
            {
                FirstName = "test",
                LastName = "testLastName",
                Email = "test@email.com",
                Position = "testPosition",
                Department = "testDeapartment",
                IsAdmin = true,
                IsActive = true,

                PermissionList = new Dictionary<Guid, PermissionDetails>()
            };

            var agg = UserAggregate.CreateNewUser(dto);
            return agg;
        }

        public void setup_session_to_ensure_GetAndCommit_are_called()
        {
            var agg = SetupAdminUser();
            SessionMock.Setup(a => a.Get<UserAggregate>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .Returns(Task.FromResult(agg));

            SessionMock.Setup(a => a.Commit(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionCommitWasCalled = true;
                })
                .Returns(Task.FromResult(0));
        }
    }
}
