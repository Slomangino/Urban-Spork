using System;
namespace UrbanSpork.Domain.Interfaces.WriteModel
{
    //<TResult> is the command's return type, generic so we can handle many types of commands
    public interface ICommand<TResult>
    {
    }
}
