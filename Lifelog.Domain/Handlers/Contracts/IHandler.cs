using Lifelog.Domain.Commands.Contracts;

namespace Lifelog.Domain.Handlers.Contracts;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}