using AutoMapper;
using UrbanSpork.CQRS.Events;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events.Users;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace UrbanSpork.DataAccess.Projections
{
    public class UserDetailProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public UserDetailProjection() { }

        public UserDetailProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key]
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "json")]
        public string Access { get; set; }

        [Column(TypeName = "json")]
        public string Equipment { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            UserDetailProjection info;
            switch (@event) { 
                case UserCreatedEvent uc:
                    info = Mapper.Map<UserDetailProjection>(uc.UserDTO);
                    _context.UserDetailProjection.Add(info);
                    break;
                case UserUpdatedEvent uu:
                    info = Mapper.Map<UserDetailProjection>(uu.UserDTO);
                    _context.UserDetailProjection.Update(info);
                    break;
                case UserDisabledEvent ud:
                    info = Mapper.Map<UserDetailProjection>(ud.UserDTO);
                    _context.UserDetailProjection.Update(info);
                    break;
                case UserEnabledEvent ue:
                    info = Mapper.Map<UserDetailProjection>(ue.UserDTO);
                    _context.UserDetailProjection.Update(info);
                    break;
                case UserPermissionsUpdatedEvent upu:
                    var user = await _context.UserDetailProjection.SingleAsync(b => b.UserID == upu.Id);
                    user.Access = JsonConvert.SerializeObject(upu.Dto.PermissionList);
                    _context.UserDetailProjection.Attach(user);
                    _context.Entry(user).Property(a => a.Access).IsModified = true;
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
