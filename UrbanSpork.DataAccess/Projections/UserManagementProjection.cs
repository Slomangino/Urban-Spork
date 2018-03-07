using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UrbanSpork.Common;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess.Projections
{
    public class UserManagementProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public UserManagementProjection() { }

        public UserManagementProjection(UrbanDbContext context)
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

        public async Task ListenForEvents(IEvent @event)
        {
            UserManagementProjection user = new UserManagementProjection();
            switch (@event)
            {
                case UserCreatedEvent uc:
                    user.UserId = uc.Id;
                    user.FirstName = uc.FirstName;
                    user.LastName = uc.LastName;
                    user.Email = uc.Email;
                    user.Position = uc.Position;
                    user.Department = uc.Department;
                    user.IsActive = uc.IsActive;
                    user.IsAdmin = uc.IsAdmin;

                    _context.UserManagementProjection.Add(user);
                    break;
                case UserUpdatedEvent uu:
                    user = await _context.UserManagementProjection.SingleAsync(b => b.UserId == uu.Id);
                    _context.UserManagementProjection.Attach(user);

                    user.FirstName = uu.FirstName;
                    user.LastName = uu.LastName;
                    user.Email = uu.Email;
                    user.Position = uu.Position;
                    user.Department = uu.Department;
                    user.IsAdmin = uu.IsAdmin;
                    _context.Entry(user).Property(a => a.FirstName).IsModified = true;
                    _context.Entry(user).Property(a => a.LastName).IsModified = true;
                    _context.Entry(user).Property(a => a.Email).IsModified = true;
                    _context.Entry(user).Property(a => a.Position).IsModified = true;
                    _context.Entry(user).Property(a => a.Department).IsModified = true;
                    _context.Entry(user).Property(a => a.IsAdmin).IsModified = true;

                    _context.UserManagementProjection.Update(user);
                    break;
                case UserDisabledEvent ud:
                    user = await _context.UserManagementProjection.SingleAsync(b => b.UserId == ud.Id);
                    _context.UserManagementProjection.Attach(user);

                    user.IsActive = ud.IsActive;
                    _context.Entry(user).Property(a => a.IsActive).IsModified = true;

                    _context.UserManagementProjection.Update(user);
                    break;
                case UserEnabledEvent ue:
                    user = await _context.UserManagementProjection.SingleAsync(b => b.UserId == ue.Id);
                    _context.UserManagementProjection.Attach(user);

                    user.IsActive = ue.IsActive;
                    _context.Entry(user).Property(a => a.IsActive).IsModified = true;

                    _context.UserManagementProjection.Update(user);
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}

