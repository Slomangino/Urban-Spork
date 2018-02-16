using System.Threading.Tasks;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.CQRS.WriteModel
{
    public interface ICommandDispatcher
    {
        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
