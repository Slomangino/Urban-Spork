using UrbanSpork.CQRS.Messages;

namespace UrbanSpork.CQRS.Commands
{
    /// <summary>
    /// Defines a handler for a command with a cancellation token.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface ICancellableCommandHandler<in T> : ICancellableHandler<T> where T : ICommand
    {
    }
}