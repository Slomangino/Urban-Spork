using UrbanSpork.CQRS.Messages;

namespace UrbanSpork.CQRS.Commands
{
    /// <summary>
    /// Defines an command with required fields.
    /// </summary>
    public interface ICommand : IMessage
    {
        int ExpectedVersion { get; }
    }
}