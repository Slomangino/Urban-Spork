using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.CommandHandlers.Permission;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests
{
    public class DisablePermissionCommandHandlerMockAggregate
    {
        public static readonly Mock<ISession> SessionMock = new Mock<ISession>();
        public static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
        public static readonly Mock<UrbanDbContext> ContextMock = new Mock<UrbanDbContext>();
        public static readonly Mock<DbSet<UserDetailProjection>> UserDetailMock = new Mock<DbSet<UserDetailProjection>>();

        public readonly ISession Session = SessionMock.Object;
        public readonly IMapper Mapper = MapperMock.Object;
        public readonly UrbanDbContext Context = ContextMock.Object;
        public readonly DbSet<UserDetailProjection> UserDetail = UserDetailMock.Object;

        public bool SessionGetWasCalled = false;
        public bool SessionCommitWasCalled = false;

        public DisablePermissionCommandHandler DisablePermissionHandlerFactory()
        {
            return new DisablePermissionCommandHandler(Mapper, Session, Context);
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

        public void setup_session_to_ensure_addAndCommit_are_called(PermissionAggregate agg)
        {
            SessionMock.Setup(a => a.Get<PermissionAggregate>(agg.Id, It.IsAny<int>(), It.IsAny<CancellationToken>()))
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

        public void setup_context_to_return_no_items()
        {
            var users = new List<UserDetailProjection>
            {

            }.AsQueryable();

            UserDetailMock.As<IQueryable<UserDetailProjection>>().Setup(m => m.Provider).Returns(users.Provider);
            UserDetailMock.As<IQueryable<UserDetailProjection>>().Setup(m => m.Expression).Returns(users.Expression);
            UserDetailMock.As<IQueryable<UserDetailProjection>>().Setup(m => m.ElementType).Returns(users.ElementType);
            UserDetailMock.As<IQueryable<UserDetailProjection>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            ContextMock.Setup(a => a.UserDetailProjection)
                .Returns(UserDetailMock.Object);
        }
    }
}
