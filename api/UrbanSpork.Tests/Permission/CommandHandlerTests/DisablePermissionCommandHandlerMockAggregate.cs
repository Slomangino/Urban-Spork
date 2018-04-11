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

        public async void setup_session_to_ensure_addAndCommit_are_called(PermissionAggregate permAagg, UserAggregate userAgg)
        {
            SessionMock.Setup(a => a.Get<PermissionAggregate>(permAagg.Id, It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .ReturnsAsync(await Task.FromResult(permAagg));

            SessionMock.Setup(a => a.Get<UserAggregate>(userAgg.Id, It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionGetWasCalled = true;
                })
                .ReturnsAsync(await Task.FromResult(userAgg));

            SessionMock.Setup(a => a.Commit(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    SessionCommitWasCalled = true;
                })
                .Returns(Task.FromResult(0));
        }

        public void setup_context_to_return_no_items()
        {
            // to return some test data, put some data in this list that is passed to MockDbSet!
            ContextMock.Setup(a => a.UserDetailProjection)
                .Returns(MockDbSet(new List<UserDetailProjection>()).Object);
        }

        // Generic mock of the DbSet from stack overflow
        // https://stackoverflow.com/questions/37630564/how-to-mock-up-dbcontext
        public Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSetMock;
        }
    }


}
