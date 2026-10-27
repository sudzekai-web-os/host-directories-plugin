using Application.Dto.Directory.Collections;

namespace Application.Dispatching.Commands
{
    public record GetDirectoryFilesCommand(string Path) : ICommand<DirectoryFilesDto>;
}
