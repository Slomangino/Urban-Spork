//using UrbanSpork.Domain.ReadModel.Repositories.Interfaces;
//using Newtonsoft.Json;
//using Npgsql;
//using System;
//using System.Json;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UrbanSpork.Domain.ReadModel;

//using Newtonsoft.Json.Linq;
//using Autofac;


////Must be changed to reflect UrbanSpork project structure

//namespace UrbanSpork.Domain.ReadModel.Repositories
//{
//    public class UserRepository : BaseRepository, IUserRepository
//    {
//        //testing here
//        const string tableName = "NotUsers";
//        //const string tableName = "users";

//         private readonly IComponentContext _context;

//        // CONSTRUCTOR 
//        //private readonly USDbContext _context;

//        //public UserRepository(USfDbContext context)
//        //{
//        //    _context = context;
//        //}

//        public UserRepository(IComponentContext context) : base(context)
//        {
//            _context = context;
//        }


//        public async Task<List<JObject>> GetAll(String tableName)
//        {
//            return await Get(tableName);
//        }

//        public async Task<List<JObject>> GetById(int userID)
//        {
//            return await Get(userID.ToString(), tableName, "user_id");
//        }

//        //public async Task<List<JObject>> GetByAttribute(string attr, string columnName)
//        //{
//        //    return await Get(tableName, attr );

//        //}

//        //public List<UserRM> GetMultiple(List<int> userIDs)
//        //{
//        //    return GetMultiple<UserRM>(userIDs);
//        //}

//        //public IEnumerable<UserRM> GetAll()
//        //{
//        //    return Get<List<UserRM>>("all");
//        //}

//        //public void Save(UserRM user)
//        //{
//        //    Save(user.userID, user);
//        //    MergeIntoAllCollection(user);
//        //}

//        //private void MergeIntoAllCollection(UserRM user)
//        //{
//        //    List<UserRM> allusers = new List<UserRM>();
//        //    if (Exists("all"))
//        //    {
//        //        allusers = Get<List<UserRM>>("all");
//        //    }

//        //    //If the district already exists in the ALL collection, remove that entry
//        //    if (allusers.Any(x => x.userID == user.userID))
//        //    {
//        //        allusers.Remove(allusers.First(x => x.userID == user.userID));
//        //    }

//        //    //Add the modified district to the ALL collection
//        //    allusers.Add(user);

//        //    Save("all", allusers);
//        //}

//        //IEnumerable<UserRM> IUserRepository.GetAll()
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //UserRM IBaseRepository<UserRM>.GetByID(int id)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //List<UserRM> IBaseRepository<UserRM>.GetMultiple(List<int> ids)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public bool Exists(int id)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public void Save(UserRM item)
//        //{
//        //    throw new NotImplementedException();
//        //}
//    }
//}




///// TESTING COMMENTS ////
///// 
///// 
/////  //while(dataReader.Read()){
////    listOfUsers.Add(new UserRM()
////    {
////        UserID = (int)dataReader[0],
////        FirstName = dataReader[1].ToString(),
////        LastName = dataReader[2].ToString(),
////        Email = dataReader[3].ToString(),
////        Position = dataReader[4].ToString(),
////        Department = dataReader[5].ToString(),
////        IsActive = (bool)dataReader[6]//,
////        //Assets = (JO) dataReader[7]
////    });
////}