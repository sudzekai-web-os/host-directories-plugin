using Application.Dispatching.Commands;
using Application.Dto.Directory.Security;
using Application.Extensions;
using Application.Services;

namespace Application.Dispatching.Handlers
{
    internal class GetDirectorySecurityInfoCommandHandler(
        DirectoryPathValidator validator) : ICommandHandler<GetDirectorySecurityInfoCommand, DirectorySecurityInfoDto>
    {
        public Task<DirectorySecurityInfoDto> HandleAsync(GetDirectorySecurityInfoCommand command)
        {
            validator.Validate(command.Path);

            DirectoryInfo di = new(command.Path);

            return Task.FromResult<DirectorySecurityInfoDto>(di.ToWindowsDirectorySecurityInfo());
        }
    }
}
