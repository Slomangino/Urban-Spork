using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Json;
using Newtonsoft.Json.Linq;
using Autofac;
using UrbanSpork.Domain.ReadModel.Repositories.Interfaces;


//Must Be Changed to reflect PostgreSQL DB Connection, rather than Redis

namespace UrbanSpork.Domain.ReadModel.Repositories
{
    public class BaseRepository : IBaseRepository<JObject>
    {
        private readonly NpgsqlConnection _postgresConnection;
        private readonly IComponentContext _context;

        // CONSTRUCTOR 
        public BaseRepository(IComponentContext context, NpgsqlConnection postgresConnection)
        {
            _postgresConnection = postgresConnection;
            _context = context;
        }

        // THIS GETS ALL
        public async Task<List<JObject>> Get(string tableName)
        {
            //NpgsqlConnection conn = _postgresConnection;

            await _postgresConnection.OpenAsync();

            //NpgsqlCommand cmd = new NpgsqlCommand($"select * from users where userid={userId}", conn);
            NpgsqlCommand cmd = new NpgsqlCommand($"select * from {tableName}", _postgresConnection);

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            List<JObject> listOfResults = new List<JObject>();

            while (dataReader.Read())
            {
                dynamic user = new JObject();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetDataTypeName(i) != "json")
                    {
                        user[dataReader.GetName(i)] = JToken.FromObject(dataReader[i]);
                    }
                    else
                    {
                        user[dataReader.GetName(i)] = JObject.Parse((string)JToken.FromObject(dataReader[i]));
                    }
                }

                listOfResults.Add(user);
            }

            //_postgresConnection.Close();
            return listOfResults;
        }


        // Gets By Id *******NOW GARBAGE*******
        public async Task<List<JObject>> Get(string val, string tableName, string column)
        {
            
            NpgsqlConnection conn = _postgresConnection;

            await conn.OpenAsync();

            //NpgsqlCommand cmd = new NpgsqlCommand($"select * from users where userid={userId}", conn);
            NpgsqlCommand cmd = new NpgsqlCommand($"select * from {tableName} where {column}={val}", conn);

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            List<JObject> listOfResults = new List<JObject>();

            while (dataReader.Read())
            {
                dynamic user = new JObject();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetDataTypeName(i) != "json")
                    {
                        user[dataReader.GetName(i)] = JToken.FromObject(dataReader[i]);
                    }
                    else
                    {
                        user[dataReader.GetName(i)] = JObject.Parse((string)JToken.FromObject(dataReader[i]));
                    }
                }

                listOfResults.Add(user);
            }


            return listOfResults;

            ////UserRM _user = new UserRM();

            //List<UserRM> listOfUsers = new List<UserRM>();

            ////List<T> list = new List<T>();

            ////while (dataReader.Read())
            ////{
            ////    //Console.WriteLine("{0}", dr[1]);
            ////    _user.UserID = int.Parse(string.Format("{0}", dataReader[0].ToString()));
            ////    _user.FirstName = dataReader[1].ToString();
            ////    _user.LastName = dataReader[2].ToString();
            ////    //list.Add(JsonConvert.DeserializeObject<T>(vals.ToString()));
            ////}

            //while(dataReader.Read()){
            //    listOfUsers.Add(new UserRM()
            //    {
            //        UserID = (int)dataReader[0],
            //        FirstName = dataReader[1].ToString(),
            //        LastName = dataReader[2].ToString(),
            //        Email = dataReader[3].ToString(),
            //        Position = dataReader[4].ToString(),
            //        Department = dataReader[5].ToString(),
            //        IsActive = (bool)dataReader[6],
            //        Assets = (JsonObject) dataReader[7]
            //    });
            //}


            ////string output = JsonConvert.SerializeObject(_user);

            ////Console.Write(output + "\n");

            ////Console.WriteLine(list.ToString());

            //conn.Close();

            ////return JsonConvert.DeserializeObject<T>(output.ToString());
            //return listOfUsers.ToArray();
        }

        // Implemented GetById
        public async Task<List<JObject>> GetById(string val, string tableName, string column)
        {

            //NpgsqlConnection conn = _postgresConnection;

            await _postgresConnection.OpenAsync();

            //NpgsqlCommand cmd = new NpgsqlCommand($"select * from users where userid={userId}", conn);
            NpgsqlCommand cmd = new NpgsqlCommand($"select * from {tableName} where {column}={val}", _postgresConnection);

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            List<JObject> listOfResults = new List<JObject>();

            while (dataReader.Read())
            {
                dynamic user = new JObject();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetDataTypeName(i) != "json")
                    {
                        user[dataReader.GetName(i)] = JToken.FromObject(dataReader[i]);
                    }
                    else
                    {
                        user[dataReader.GetName(i)] = JObject.Parse((string)JToken.FromObject(dataReader[i]));
                    }
                }

                listOfResults.Add(user);
            }


            return listOfResults;
        }

        // OVERLOAD HERE


        //    public List<T> GetMultiple<T>(List<int> ids)
        //    {
        //        _pgsqlConnection.Open();

        //        NpgsqlCommand command = new NpgsqlCommand("select * from users where userID = ANY(:numbers)");
        //        NpgsqlParameter p = new NpgsqlParameter("numbers", NpgsqlDbType.Array | NpgsqlDbType.Numeric);
        //        p.Value = ids.ToArray();
        //        command.Parameters.Add(p);

        //        // Execute the query and obtain a result set
        //        NpgsqlDataReader dataReader = command.ExecuteReader();

        //        List<T> items = new List<T>();

        //        //while (dataReader.Read())
        //        //    items.Add();

        //        //dataReader.GetEnumerator



        //            items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
        //        }
        //        return items;
        //    }

        //    public bool Exists(int id)
        //    {
        //        return Exists(id.ToString());
        //    }

        //    public bool Exists(string keySuffix)
        //    {
        //        var key = MakeKey(keySuffix);
        //        var database = _pgsqlConnection.Open();
        //        var serializedObject = database.StringGet(key);
        //        return !serializedObject.IsNullOrEmpty;
        //    }

        //    public void Save(int id, object entity)
        //    {
        //        Save(id.ToString(), entity);
        //    }

        //    public void Save(string keySuffix, object entity)
        //    {
        //        var key = MakeKey(keySuffix);
        //        var database = _redisConnection.GetDatabase();
        //        database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        //    }

        //    private string MakeKey(int id)
        //    {
        //        return MakeKey(id.ToString());
        //    }

        //    private string MakeKey(string keySuffix)
        //    {
        //        if (!keySuffix.StartsWith(_namespace + ":"))
        //        {
        //            return _namespace + ":" + keySuffix;
        //        }
        //        else return keySuffix; //Key is already suffixed with namespace
        //    }
    }
}
