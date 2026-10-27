using Application.Dto.Directory.Collections;

namespace Application.Dispatching.Commands
{
    public record class GetDirectoryDirectoriesCommand(string Path) : ICommand<DirectoryDirectoriesDto>;
}
