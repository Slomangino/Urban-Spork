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
using UrbanSpork.WriteModel.CommandHandlers;

namespace UrbanSpork.Tests.User.CommandHandlerTests
{
    public class CreateSingleUserCommandHandlerMockAggregate
    {
        public static readonly Mock<ISession> SessionMock = new Mock<ISession>();
        public static readonly Mock<IEmail> EmailMock = new Mock<IEmail>();
        public static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();

        public readonly ISession Session = SessionMock.Object;
        public readonly IEmail Email = EmailMock.Object;
        public readonly IMapper Mapper = MapperMock.Object;

        public bool SessionAddWasCalled = false;
        public bool SessionCommitWasCalled = false;

        public CreateSingleUserCommandHandler HandlerFactory()
        {
            return new CreateSingleUserCommandHandler(Session, Email, Mapper);
        }

        public void setup_session_to_ensure_addAndCommit_are_called()
        {
            SessionMock.Setup(a => a.Add(It.IsAny<UserAggregate>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionAddWasCalled = true;
                })
                .Returns(Task.FromResult(0));

            SessionMock.Setup(a => a.Commit(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionCommitWasCalled = true;
                })
                .Returns(Task.FromResult(0));
        }
    }
}
