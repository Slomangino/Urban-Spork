using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Npgsql;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.ReadModel.Repositories.Interfaces;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Task<List<JObject>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<JObject>> Handle(GetAllUsersQuery query)
        {

            NpgsqlConnection conn = ConnectionFactory.GetUserConnection();

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



            //await _userRepository.Get(query.TableName);
            //return await _userRepository.GetAll(query.TableName);
        }
    }
}
