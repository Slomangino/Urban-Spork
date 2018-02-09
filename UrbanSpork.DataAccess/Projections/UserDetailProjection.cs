using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AutoMapper;
using CQRSlite.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events.Users;

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

        [Column(TypeName = "date")]
        public DateTimeOffset DateCreated { get; set; }

        [Column(TypeName = "json")]
        public string Access { get; set; }

        [Column(TypeName = "json")]
        public string Equipment { get; set; }

        public async void ListenForEvents(IEvent @event)
        {

            switch (@event) { 
                case UserCreatedEvent uc:
                    var info = Mapper.Map<UserDetailProjection>(uc.UserDTO);
                    await _context.UserDetailProjection.AddAsync(info);
                    break;
                case UserUpdatedEvent uu:
                    Console.WriteLine("User updated");
                    break;
            }
            await _context.SaveChangesAsync();
        }
    }
}
