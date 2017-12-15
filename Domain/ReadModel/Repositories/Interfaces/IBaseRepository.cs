using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Must be changed to reflect UrbanSpork project structure

namespace UrbanSpork.Domain.ReadModel.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetById(int id);
        //List<T> GetMultiple(List<int> ids);
        //bool Exists(int id);
        //void Save(T item);
    }
}