using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLitePCL;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.DataAccess.Projections
{
    public class PermissionTemplateProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public PermissionTemplateProjection(UrbanDbContext context)
        {
            _context = context;
        }

        public PermissionTemplateProjection() { }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "json")]
        public string TemplatePermissions { get; set; }
        

        public async Task ListenForEvents(IEvent @event)
        {
            //listen for permission named changed, 
            //permission disabled also needs to remove that permission from the template
            switch (@event)
            {
                case PermissionInfoUpdatedEvent pu:
                    var p = await _context.PermissionTemplateProjection.Where(a => 
                        JsonConvert.DeserializeObject<Dictionary<Guid, string>>(a.TemplatePermissions).ContainsKey(pu.Id)).ToListAsync();
                    if (p.Any())
                    {
                        foreach (var entry in p)
                        {
                            var permissions =
                                JsonConvert.DeserializeObject<Dictionary<Guid, string>>(entry.TemplatePermissions);
                            permissions[pu.Id] = pu.Name;
                            entry.TemplatePermissions = JsonConvert.SerializeObject(permissions);

                            _context.PermissionTemplateProjection.Update(entry);
                        }
                    }
                    break;

                case PermissionDisabledEvent pd:
                    var templates = await _context.PermissionTemplateProjection.Where(a =>
                        JsonConvert.DeserializeObject<Dictionary<Guid, string>>(a.TemplatePermissions).ContainsKey(pd.Id)).ToListAsync();
                    if (templates.Any())
                    {
                        foreach (var entry in templates)
                        {
                            var permissions =
                                JsonConvert.DeserializeObject<Dictionary<Guid, string>>(entry.TemplatePermissions);
                            permissions.Remove(pd.Id);

                            entry.TemplatePermissions = JsonConvert.SerializeObject(permissions);
                            _context.PermissionTemplateProjection.Update(entry);
                        }
                    }

                    break;
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
