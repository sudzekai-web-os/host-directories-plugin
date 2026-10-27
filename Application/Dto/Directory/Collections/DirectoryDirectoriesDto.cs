using Application.Dto.Directory.Info;

namespace Application.Dto.Directory.Collections
{
    public record DirectoryDirectoriesDto(
        DirectoryInfoSimpleDto[] Directories
    );
}
