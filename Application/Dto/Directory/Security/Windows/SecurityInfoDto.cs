namespace Application.Dto.Directory.Security.Windows
{
    public record SecurityInfoDto(
        bool FullControl,
        bool Modify,
        bool ReadAndExecute,
        bool ListDirectory,
        bool Read,
        bool Write);
}
