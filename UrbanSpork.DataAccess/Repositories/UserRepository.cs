using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.DataAccess;

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
            //var user = _context.Users.Single(b => b.UserID == id);
            //return Task.FromResult(Mapper.Map<UserDTO>(user));
            return Task.FromResult(new UserDTO());
        }

        public Task<List<UserDTO>> GetAllUsers()
        {
            //var users = _context.Users.ToList();
            //var userList = new List<UserDTO>();
            //foreach(var user in users)
            //{
            //    var userDto = Mapper.Map<UserDTO>(user);
            //    userList.Add(userDto);
            //}
            //return Task.FromResult(userList);
            return Task.FromResult(new List<UserDTO>());
        }

        public void CreateUser(User user)
        {
            //_context.Users.Add(user);
            //_context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            //_context.Users.Update(user);
            //_context.SaveChanges();
        }

        //public async void SaveEvent(IEvent[] events)
        //{
        //    foreach (IEvent e in events)
        //    {
        //        var serializedEvent = BuildEventRowFromEvent(e);
        //        await _context.Events.AddAsync(serializedEvent);
        //    }

        //}

        //private EventStoreDataRow BuildEventRowFromEvent(IEvent e)
        //{
        //    var eventData = new EventStoreDataRow
        //    {
        //        Id = e.Id,
        //        Version = e.Version,
        //        EventType = e.GetType().FullName,
        //        EventData = SerializeEvent(e)
        //    };
        //    return eventData;
        //}

        //public string SerializeEvent(IEvent e)
        //{
        //    return JsonConvert.SerializeObject(e);
        //}
    }
}
