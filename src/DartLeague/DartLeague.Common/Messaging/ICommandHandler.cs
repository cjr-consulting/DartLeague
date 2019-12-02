using System;

namespace DartLeague.Common.Messaging
{
    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        TResult Handle(TCommand command);
    }
}
