using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Npgsql;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Task<List<UserDTO>>>
    {
        public GetAllUsersQueryHandler()//USDbContext context)
        {
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery query)
        {
            /*NpgsqlConnection conn = new NpgsqlConnection("Host=urbansporkdb.cj0fybtxusp9.us-east-1.rds.amazonaws.com;Port=5405;User Id=yamnel;Password=urbansporkpass;Database=urbansporkdb");
            await conn.OpenAsync();
            //NpgsqlCommand cmd = new NpgsqlCommand($"select * from users where userid={userId}", conn);
            NpgsqlCommand cmd = new NpgsqlCommand($"select * from {query.TableName}", conn);
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
                    }else
                    {
                        user[dataReader.GetName(i)] = JObject.Parse((string)JToken.FromObject(dataReader[i]));
                    }
                }

                listOfResults.Add(user);
            }

            conn.Close();
            return listOfResults;*/

            /*using var context = new USDbContext()
            {

            }*/
                
            return new List<UserDTO>();

            //await _userRepository.Get(query.TableName);
            //return await _userRepository.GetAll(query.TableName);
        }

        Task<Task<List<UserDTO>>> IQueryHandler<GetAllUsersQuery, Task<List<UserDTO>>>.Handle(GetAllUsersQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
