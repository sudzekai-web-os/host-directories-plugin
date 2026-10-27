namespace Application.Dto.Directory.Security.Windows
{
    public record SecurityGroupDto(
        string Name, 
        bool FullControl,
        bool Modify,
        bool ReadAndExecute,
        bool ListDirectory,
        bool Read,
        bool Write
    );
}
