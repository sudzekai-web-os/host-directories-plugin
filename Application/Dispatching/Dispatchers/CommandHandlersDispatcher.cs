using Application.Dispatching.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Dispatching.Dispatchers
{
    internal class CommandHandlersDispatcher(IServiceProvider provider) : ICommandDispatcher
    {
        async Task<TResult> ICommandDispatcher.DispatchAsync<TCommand, TResult>(TCommand command)
        {
            var service = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

            return await service.HandleAsync(command);
        }
    }
}
