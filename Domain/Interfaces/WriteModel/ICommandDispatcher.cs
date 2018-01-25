using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.Interfaces.WriteModel
{
    public interface ICommandDispatcher
    {
        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
