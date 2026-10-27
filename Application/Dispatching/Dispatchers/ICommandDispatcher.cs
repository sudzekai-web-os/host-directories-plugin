using Application.Dispatching.Commands;

namespace Application.Dispatching.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
            where TCommand : class, ICommand<TResult>
            where TResult : class;
    }
}
