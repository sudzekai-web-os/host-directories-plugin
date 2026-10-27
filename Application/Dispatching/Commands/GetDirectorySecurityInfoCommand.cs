using Application.Dto.Directory.Security;

namespace Application.Dispatching.Commands
{
    public record GetDirectorySecurityInfoCommand(string Path) : ICommand<DirectorySecurityInfoDto>;
}
