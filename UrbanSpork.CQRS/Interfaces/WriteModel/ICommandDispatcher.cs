using System.Threading.Tasks;

namespace UrbanSpork.CQRS.Interfaces.WriteModel
{
    public interface ICommandDispatcher
    {
        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
