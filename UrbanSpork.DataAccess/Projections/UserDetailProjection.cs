using AutoMapper;
using UrbanSpork.CQRS.Events;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events.Users;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using UrbanSpork.Common;

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
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        [Column(TypeName = "json")]
        public string PermissionList { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateCreated { get; set; }

        //[Column(TypeName = "json")]
        //public string Access { get; set; }

        //[Column(TypeName = "json")]
        //public string Equipment { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            UserDetailProjection info = new UserDetailProjection();
            switch (@event) { 
                case UserCreatedEvent uc:
                    info.UserId = uc.Id;
                    info.FirstName = uc.FirstName;
                    info.LastName = uc.LastName;
                    info.Email = uc.Email;
                    info.Position = uc.Position;
                    info.Department = uc.Department;
                    info.IsActive = uc.IsActive;
                    info.IsAdmin = uc.IsAdmin;
                    info.PermissionList = JsonConvert.SerializeObject(uc.PermissionList);
                    info.DateCreated = uc.TimeStamp;

                    _context.UserDetailProjection.Add(info);
                    break;
                case UserUpdatedEvent uu: info the middle of stuufff/ /derp
                    info.FirstName = uu.FirstName;
                    info.LastName = uu.LastName;
                    info.Email = uu.Email;
                    info.Position = uu.Position;
                    info.Department = uu.Department;
                    info.IsAdmin = uu.IsAdmin;
                    _context.UserDetailProjection.Update(info);
                    break;
                    //case UserDisabledEvent ud:
                    //    info = Mapper.Map<UserDetailProjection>(ud.UserDTO);
                    //    _context.UserDetailProjection.Update(info);
                    //    break;
                    //case UserEnabledEvent ue:
                    //    info = Mapper.Map<UserDetailProjection>(ue.UserDTO);
                    //    _context.UserDetailProjection.Update(info);
                    //    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
