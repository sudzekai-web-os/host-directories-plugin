using Application.Dispatching.Commands;
using Application.Dto.Directory.Info;
using Application.Extensions;
using Application.Services;

namespace Application.Dispatching.Handlers
{
    internal class CreateDirectoryCommandHandler(
        DirectoryPathValidator validator) : ICommandHandler<CreateDirectoryCommand, DirectoryGeneralInfoDto>
    {
        public Task<DirectoryGeneralInfoDto> HandleAsync(CreateDirectoryCommand command)
        {
            validator.ThrowIfNotFullyQualified(command.Path);
            validator.ThrowIfExists(command.Path);

            var created = Directory.CreateDirectory(command.Path);

            return Task.FromResult(created.ToGeneralInfoDto());
        }
    }
}
