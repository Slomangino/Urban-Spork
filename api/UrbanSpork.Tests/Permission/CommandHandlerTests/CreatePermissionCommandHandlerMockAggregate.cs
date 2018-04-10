using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.WriteModel.CommandHandlers.Permission;
using UrbanSpork.WriteModel.CommandHandlers.PermissionActions;
using UrbanSpork.WriteModel.Commands.Permission;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class CreatePermissionCommandHandlerMockAggregate
    {
        public static readonly Mock<ISession> SessionMock = new Mock<ISession>();
        public static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();

        public readonly ISession Session = SessionMock.Object;
        public readonly IMapper Mapper = MapperMock.Object;

        public bool SessionAddWasCalled = false;
        public bool SessionCommitWasCalled = false;

        public CreatePermissionCommandHandler CreatePermissionHandlerFactory()
        {
            return new CreatePermissionCommandHandler(Mapper, Session);
        }

        public void setup_session_to_return_correct_aggregate()
        {
            SessionMock.Setup(a => a.Add(It.IsAny<PermissionAggregate>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionAddWasCalled = true;
                })
                .Returns(Task.FromResult(0));

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
