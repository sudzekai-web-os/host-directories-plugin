using Application.Dispatching.Commands;
using Application.Dto.Directory.Collections;
using Application.Extensions;
using Application.Services;

namespace Application.Dispatching.Handlers
{
    internal class GetDirectoryFilesCommandHandler(
        DirectoryPathValidator validator)
        : ICommandHandler<GetDirectoryFilesCommand, DirectoryFilesDto>
    {
        public Task<DirectoryFilesDto> HandleAsync(GetDirectoryFilesCommand command)
        {
            validator.Validate(command.Path);

            DirectoryInfo di = new(command.Path);

            return Task.FromResult(di.ToFilesDto());
        }
    }
}
