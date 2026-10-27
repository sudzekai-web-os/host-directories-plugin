using Application.Dispatching.Commands;
using Application.Dto.Directory.Collections;
using Application.Extensions;
using Application.Services;

namespace Application.Dispatching.Handlers
{
    internal class GetDirectoryDirectoriesCommandHandler(
        DirectoryPathValidator validator) : ICommandHandler<GetDirectoryDirectoriesCommand, DirectoryDirectoriesDto>
    {
        public Task<DirectoryDirectoriesDto> HandleAsync(GetDirectoryDirectoriesCommand command)
        {
            validator.Validate(command.Path);

            DirectoryInfo di = new(command.Path);

            return Task.FromResult(di.ToDirectoriesDto());
        }
    }
}
