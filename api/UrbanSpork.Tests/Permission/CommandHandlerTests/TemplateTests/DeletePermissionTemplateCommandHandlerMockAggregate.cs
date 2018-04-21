using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Newtonsoft.Json;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.CommandHandlers.PermissionTemplates;

namespace UrbanSpork.Tests.Permission.CommandHandlerTests.TemplateTests
{
    public class DeletePermissionTemplateCommandHandlerMockAggregate
    {
        public static readonly Mock<UrbanDbContext> ContextMock = new Mock<UrbanDbContext>();

        public readonly UrbanDbContext Context = ContextMock.Object;

        public bool ContextRemoveWasCalled = false;
        public bool ContextSaveWasCalled = false;

        public DeletePermissionTemplateCommandHandler DeletePermissionTemplateHandlerFactory()
        {
            return new DeletePermissionTemplateCommandHandler(Context);
        }

        public PermissionTemplateProjection SetupTestPermissionTemplate()
        {
            var createPermisisonDTO = new CreateNewPermissionDTO
            {
                Name = "testPermisison",
                Description = "testDescription",
                IsActive = true,
                Image = "testUrl"
            };

            var permAgg = PermissionAggregate.CreateNewPermission(createPermisisonDTO);

            var permissionTemplate = new PermissionTemplateProjection
            {
                Id = new Guid(),
                Name = "testName",
                TemplatePermissions = JsonConvert.SerializeObject(new Dictionary<Guid, string>
                {
                    {permAgg.Id, permAgg.Name}
                })
            };

            return permissionTemplate;
        }

        public async void setup_context_to_return_one_item(PermissionTemplateProjection proj)
        {
            var list = new List<PermissionTemplateProjection>
            {
                proj
            };

            ContextMock.Setup(a => a.PermissionTemplateProjection)
                .Returns(MockDbSetFactory.MockDbSet(list).Object);

            ContextMock.Setup(a => a.PermissionTemplateProjection.Remove(proj))
                .Callback<PermissionTemplateProjection>((a) =>
                {
                    list.Remove(a);
                    ContextRemoveWasCalled = true;
                })
                .Returns((EntityEntry<PermissionTemplateProjection>)null);

            ContextMock.Setup(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    ContextSaveWasCalled = true;
                })
                .ReturnsAsync(await Task.FromResult(0));
        }
    }
}
