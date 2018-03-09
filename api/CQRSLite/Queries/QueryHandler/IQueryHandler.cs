using System.Threading.Tasks;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.CQRS.Queries.QueryHandler
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
