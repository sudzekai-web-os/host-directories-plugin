using Application.Dto.File;

namespace Application.Dto.Directory.Collections
{
    public record DirectoryFilesDto(
        FileInfoSimpleDto[] Files
    );
}
