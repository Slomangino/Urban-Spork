using System.Threading.Tasks;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.CQRS.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
