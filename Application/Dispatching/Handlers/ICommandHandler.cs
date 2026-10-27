using Application.Dispatching.Commands;

namespace Application.Dispatching.Handlers
{
    internal interface ICommandHandler<TCommand, TResult>
        where TCommand : class, ICommand<TResult>
        where TResult : class
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
