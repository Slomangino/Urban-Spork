using UrbanSpork.Domain.ReadModel.Repositories.Interfaces;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Domain.ReadModel;

//Must be changed to reflect UrbanSpork project structure

namespace CQRSLiteDemo.Domain.ReadModel.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(NpgsqlConnection pgsqlsConnection) : base(pgsqlsConnection, "user") { }

        public UserRM GetByID(int userID)
        {
            return Get<UserRM>(userID);
        }

        public List<UserRM> GetMultiple(List<int> userIDs)
        {
            return GetMultiple<UserRM>(userIDs);
        }

        public IEnumerable<UserRM> GetAll()
        {
            return Get<List<UserRM>>("all");
        }

        public void Save(UserRM user)
        {
            Save(user.userID, user);
            MergeIntoAllCollection(user);
        }

        private void MergeIntoAllCollection(UserRM user)
        {
            List<UserRM> allusers = new List<UserRM>();
            if (Exists("all"))
            {
                allusers = Get<List<UserRM>>("all");
            }

            //If the district already exists in the ALL collection, remove that entry
            if (allusers.Any(x => x.userID == user.userID))
            {
                allusers.Remove(allusers.First(x => x.userID == user.userID));
            }

            //Add the modified district to the ALL collection
            allusers.Add(user);

            Save("all", allusers);
        }

        IEnumerable<UserRM> IUserRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        UserRM IBaseRepository<UserRM>.GetByID(int id)
        {
            throw new NotImplementedException();
        }

        List<UserRM> IBaseRepository<UserRM>.GetMultiple(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(UserRM item)
        {
            throw new NotImplementedException();
        }
    }
}
