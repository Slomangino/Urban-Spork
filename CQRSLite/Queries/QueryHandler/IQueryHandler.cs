using CQRSLite.Queries.Query;
using System.Threading.Tasks;

namespace CQRSLite.Queries.QueryHandler
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
