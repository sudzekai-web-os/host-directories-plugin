namespace Application.Dto.Directory.Security.Windows
{
    public record WindowsDirectorySecurityInfo(
        string Name,
        string FullName,
        SecurityGroupDto[] SecurityGroups
    ) : DirectorySecurityInfoDto;
}
