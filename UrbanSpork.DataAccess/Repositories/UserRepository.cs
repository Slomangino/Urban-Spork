using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.CQRS.Interfaces.Events;
using Newtonsoft.Json;

namespace UrbanSpork.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UrbanDbContext _context;

        public UserRepository(UrbanDbContext context)
        {
            _context = context;
            
        }

        public Task<UserDTO> GetSingleUser(Guid id)
        {
            var foo = _context.Users
                .Single(b => b.UserID == id);

            return Task.FromResult(Mapper.Map<UserDTO>(foo));
        }

        public Task<List<UserDTO>> GetAllUsers()
        {
            var users = _context.Users.ToList();
            var userList = new List<UserDTO>();
            foreach(var user in users)
            {
                var userDto = Mapper.Map<UserDTO>(user);
                userList.Add(userDto);
            }
            return Task.FromResult(userList);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            //_context.FindAsync(message);
            //return Task.FromResult(message);
        }

        public  void UpdateUser(User user)
        {
            _context.Users.Update(user);
             _context.SaveChanges();
        }

        public async void SaveEvent(IEvent[] events)
        {
            foreach (IEvent e in events)
            {

                var serializedEvent = BuildEventRowFromEvent(e);
                await _context.Events.AddAsync(serializedEvent);  ////needs to be done in userRepository
            }

        }

        private EventStoreDataRow BuildEventRowFromEvent(IEvent e)
        {
            var eventData = new EventStoreDataRow
            {
                Id = e.Id,
                Version = e.Version,
                EventType = e.GetType().FullName,
                EventData = SerializeEvent(e)
            };
            return eventData;
        }

        public string SerializeEvent(IEvent e)
        {
            var ev = JsonConvert.SerializeObject(e);
            return ev;

        }


    }
}
