using System.Threading.Tasks;

namespace UrbanSpork.CQRS.Interfaces.ReadModel
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
