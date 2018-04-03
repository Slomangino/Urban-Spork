using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;

namespace UrbanSpork.WriteModel.CommandHandlers.PermissionTemplates
{
    public class CreatePermissionTemplateCommandHandler : ICommandHandler<CreatePermissionTemplateCommand, PermissionTemplateDTO>
    {
        private readonly UrbanDbContext _context;

        public CreatePermissionTemplateCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionTemplateDTO> Handle(CreatePermissionTemplateCommand command)
        {
            PermissionTemplateProjection template = new PermissionTemplateProjection
            {
                Id = Guid.NewGuid(),
                Name = command.Input.Name,
                TemplatePermissions = command.Input.TemplatePermissions.Any() ? 
                    JsonConvert.SerializeObject(command.Input.TemplatePermissions) : 
                    JsonConvert.SerializeObject(new Dictionary<Guid, string>()),
            };

            await _context.PermissionTemplateProjection.AddAsync(template);
            await _context.SaveChangesAsync();

            var returnTemplate = new PermissionTemplateDTO
            {
                Id = template.Id,
                Name = template.Name,
                TemplatePermissions = command.Input.TemplatePermissions,
            };

            return returnTemplate;
        }
    }
}
