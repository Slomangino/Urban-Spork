using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.Interfaces.ReadModel
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
