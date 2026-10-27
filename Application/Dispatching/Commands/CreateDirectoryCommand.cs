using Application.Dto.Directory.Info;

namespace Application.Dispatching.Commands
{
    public record CreateDirectoryCommand(string Path) : ICommand<DirectoryGeneralInfoDto>;
}
