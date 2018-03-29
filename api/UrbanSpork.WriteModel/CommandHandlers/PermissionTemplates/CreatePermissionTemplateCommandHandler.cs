using System;
using System.Collections.Generic;
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
                Id = new Guid(),
                Name = command.Input.Name,
                TemplatePermissions = JsonConvert.SerializeObject(command.Input.TemplatePermissions),
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
