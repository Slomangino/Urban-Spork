using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.SLCQRS.ReadModel
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
