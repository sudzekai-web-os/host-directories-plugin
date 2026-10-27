using Application.Dispatching.Commands;
using Application.Dto.Directory.Info;
using Application.Extensions;
using Application.Services;

namespace Application.Dispatching.Handlers
{
    internal class GetDirectoryGeneralInfoCommandHandler(
        DirectoryPathValidator validator) : ICommandHandler<GetDirectoryGeneralInfoCommand, DirectoryGeneralInfoDto>
    {
        public Task<DirectoryGeneralInfoDto> HandleAsync(GetDirectoryGeneralInfoCommand command)
        {
            validator.Validate(command.Path);

            DirectoryInfo di = new(command.Path);

            return Task.FromResult(di.ToGeneralInfoDto());
        }
    }
}
