using System.Numerics;

namespace Application.Dto.Directory.Info
{
    public record DirectoryGeneralInfoDto(
        string Name,
        string FullName,
        long Length,
        DateTime CreationTimeUtc,
        DateTime LastAccessTimeUtc,
        DateTime LastWriteTimeUtc,
        long FilesCount,
        long DirectoriesCount);
}
