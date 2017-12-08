using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.ReadModel.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserRM>
    {
        IEnumerable<UserRM> GetAll();
    }
}
