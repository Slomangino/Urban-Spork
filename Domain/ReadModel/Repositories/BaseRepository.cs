using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Must Be Changed to reflect PostgreSQL DB Connection, rather than Redis

namespace UrbanSpork.Domain.ReadModel.Repositories
{
    public class BaseRepository
    {
        private readonly NpgsqlConnection _pgsqlConnection;

        /// <summary>
        /// The Namespace is the first part of any key created by this Repository, e.g. "location" or "employee"
        /// </summary>
        private readonly string _namespace;

        public BaseRepository(NpgsqlConnection pgsql, string nameSpace)
        {
            _pgsqlConnection = pgsql;
            _namespace = nameSpace;
        }

        public T Get<T>(int id)
        {
            return Get<T>(id.ToString());
        }

        public T Get<T>(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            _pgsqlConnection.Open();
            
            var serializedObject = database.StringGet(key);
            if (serializedObject.IsNullOrEmpty) throw new ArgumentNullException(); //Throw a better exception than this, please
            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public List<T> GetMultiple<T>(List<int> ids)
        {
            _pgsqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand("select * from users where userID = ANY(:numbers)");
            NpgsqlParameter p = new NpgsqlParameter("numbers", NpgsqlDbType.Array | NpgsqlDbType.Numeric);
            p.Value = ids.ToArray();
            command.Parameters.Add(p);

            // Execute the query and obtain a result set
            NpgsqlDataReader dataReader = command.ExecuteReader();

            List<T> items = new List<T>();

            //while (dataReader.Read())
            //    items.Add();

            //dataReader.GetEnumerator



                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public bool Exists(int id)
        {
            return Exists(id.ToString());
        }

        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _pgsqlConnection.Open();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }

        public void Save(int id, object entity)
        {
            Save(id.ToString(), entity);
        }

        public void Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        private string MakeKey(int id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
            {
                return _namespace + ":" + keySuffix;
            }
            else return keySuffix; //Key is already suffixed with namespace
        }
    }
}
