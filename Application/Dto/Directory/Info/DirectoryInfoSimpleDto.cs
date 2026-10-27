namespace Application.Dto.Directory.Info
{
    public record DirectoryInfoSimpleDto(
        string Name,
        string FullName,
        DateTime LastWriteTimeUtc
    );
}
