using CQRSLite.Queries.Query;
using System.Threading.Tasks;

namespace CQRSLite.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
