using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Newtonsoft.Json;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.CommandHandlers.Permission;
using UrbanSpork.WriteModel.CommandHandlers.PermissionTemplates;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests.TemplateTests
{
    public class CreatePermissionTemplateCommandHandlerMockAggregate
    {
        public static readonly Mock<UrbanDbContext> ContextMock = new Mock<UrbanDbContext>();

        public readonly UrbanDbContext Context = ContextMock.Object;

        public bool ContextAddWasCalled = false;
        public bool ContextSaveWasCalled = false;

        public CreatePermissionTemplateCommandHandler CreatePermissionTemplateHandlerFactory()
        {
            return new CreatePermissionTemplateCommandHandler(Context);
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

        public async void setup_context_to_return_one_item(PermissionAggregate agg)
        {
            var list = new List<PermissionTemplateProjection>
            {
                new PermissionTemplateProjection
                {
                    Id = Guid.Empty,
                    Name = "testTemplateName",
                    TemplatePermissions = JsonConvert.SerializeObject(
                        new Dictionary<Guid, string>
                        {
                            {agg.Id, agg.Name }
                        })
                }
            };

            ContextMock.Setup(a => a.PermissionTemplateProjection)
                .Returns(MockDbSetFactory.MockDbSet(list).Object);

            ContextMock.Setup(a => a.PermissionTemplateProjection.AddAsync(It.IsAny<PermissionTemplateProjection>(), It.IsAny<CancellationToken>()))
                .Callback<PermissionTemplateProjection, CancellationToken>((a, b) =>
                {
                    list.Add(a);
                    ContextAddWasCalled = true;
                })
                .Returns(Task.FromResult((EntityEntry<PermissionTemplateProjection>)null));

            ContextMock.Setup(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    ContextSaveWasCalled = true;
                })
                .ReturnsAsync(await Task.FromResult(0));
        }
    }
}
