using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.SLCQRS.WriteModel
{
    public interface ICommandDispatcher
    {
        TResult Execute<TResult>(ICommand<TResult> command);
    }
}
