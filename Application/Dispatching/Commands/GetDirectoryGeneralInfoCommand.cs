using Application.Dto.Directory.Info;

namespace Application.Dispatching.Commands
{
    public record GetDirectoryGeneralInfoCommand(string Path) : ICommand<DirectoryGeneralInfoDto>;
}
