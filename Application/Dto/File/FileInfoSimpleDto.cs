namespace Application.Dto.File
{
    public record FileInfoSimpleDto(
        string Name,
        string FullName,
        string Extension,
        long Length,
        DateTime CreationTimeUtc,
        DateTime LastWriteTimeUtc
    );
}
