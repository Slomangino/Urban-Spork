using System.Linq;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Filters;
using UrbanSpork.CQRS.Queries.Query;


namespace UrbanSpork.CQRS.Queries.QueryHandler
{
    public interface IFilteredQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
        IQueryable<TResult> Filter(IFilterCriteria filter);
    }
}