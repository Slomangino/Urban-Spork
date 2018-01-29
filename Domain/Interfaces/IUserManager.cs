using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<Guid> CreateNewUser();
    }
}
