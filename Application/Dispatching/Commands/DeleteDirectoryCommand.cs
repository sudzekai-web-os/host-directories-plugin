namespace Application.Dispatching.Commands
{
    public record DeleteDirectoryCommand(string FullName, bool IsRecursive) : ICommand<Unit>;
}
