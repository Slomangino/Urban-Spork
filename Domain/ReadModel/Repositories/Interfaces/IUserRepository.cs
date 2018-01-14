using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UrbanSpork.Domain.ReadModel.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<JObject>
    {
        Task<List<JObject>> GetAll(String tableName);
    }
}
