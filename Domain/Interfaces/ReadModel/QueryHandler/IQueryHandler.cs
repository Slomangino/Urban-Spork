using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.Interfaces.ReadModel
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
