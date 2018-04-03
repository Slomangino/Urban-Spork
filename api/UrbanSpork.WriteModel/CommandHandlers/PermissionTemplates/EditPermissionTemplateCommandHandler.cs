using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;

namespace UrbanSpork.WriteModel.CommandHandlers.PermissionTemplates
{
    public class EditPermissionTemplateCommandHandler : ICommandHandler<EditPermissionTemplateCommand, PermissionTemplateDTO>
    {
        private readonly UrbanDbContext _context;

        public EditPermissionTemplateCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionTemplateDTO> Handle(EditPermissionTemplateCommand command)
        {
            PermissionTemplateProjection template = await _context.PermissionTemplateProjection.SingleOrDefaultAsync(a => a.Id == command.Input.Id);

            if (template == null)
            {
                return new PermissionTemplateDTO();
            }

            template.Name = command.Input.Name;
            template.TemplatePermissions = command.Input.TemplatePermissions.Any()
                    ? JsonConvert.SerializeObject(command.Input.TemplatePermissions) : JsonConvert.SerializeObject(new Dictionary<Guid, string>());

            _context.PermissionTemplateProjection.Update(template);
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
