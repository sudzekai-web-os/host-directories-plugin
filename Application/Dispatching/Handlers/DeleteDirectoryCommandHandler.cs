using Application.Dispatching.Commands;
using Application.Services;
using Application.Types.Exceptions.Client;

namespace Application.Dispatching.Handlers
{
    internal class DeleteDirectoryCommandHandler(
        DirectoryPathValidator validator) : ICommandHandler<DeleteDirectoryCommand, Unit>
    {
        public Task<Unit> HandleAsync(DeleteDirectoryCommand command)
        {
            validator.ThrowIfNotFullyQualified(command.FullName);
            validator.ThrowIfNotExists(command.FullName);

            var di = new DirectoryInfo(command.FullName);

            if (!command.IsRecursive)
            {
                if (di.EnumerateFiles().Any() || di.EnumerateDirectories().Any())
                    throw new DirectoryNotEmptyException(command.FullName);
            }

            try
            {
                di.Delete(command.IsRecursive);
            }
            catch (UnauthorizedAccessException)
            {
                throw new DirectoryDeletionForbiddenException(command.FullName);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
