using AutoMapper;
using UrbanSpork.CQRS.Events;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "timestamp")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "json")]
        public string Access { get; set; }

        [Column(TypeName = "json")]
        public string Equipment { get; set; }

        public void ListenForEvents(IEvent @event)
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
                    Console.WriteLine("User updated");
                    break;
            }
        }
    }
}
