using Application.Dto.Directory.Collections;
using Application.Dto.Directory.Info;
using Application.Dto.Directory.Security.Windows;
using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Application.Extensions
{
    internal static class DirectoryInfoMappingExtensions
    {
        extension(DirectoryInfo di)
        {
            public DirectoryGeneralInfoDto ToGeneralInfoDto()
            {
                var measurement = di.Measure();

                return new(
                    di.Name,
                    di.FullName,
                    measurement.Length,
                    di.CreationTimeUtc,
                    di.LastAccessTimeUtc,
                    di.LastWriteTimeUtc,
                    measurement.FilesCount,
                    measurement.DirectoriesCount
                );
            }

            [SuppressMessage("OS support", "CA1416", Justification = "Обработано проверкой ОС в начале метода")]
            public WindowsDirectorySecurityInfo ToWindowsDirectorySecurityInfo()
            {
                if (!OperatingSystem.IsWindows())
                    throw new NotSupportedException("Server OS doesn't support WindowsDirectorySecurityInfoDto creation");

                DirectorySecurity ds = di.GetAccessControl();

                return new(
                    di.Name,
                    di.FullName,
                    [.. ds.GetAccessRules(true, true, typeof(SecurityIdentifier))
                    .Cast<FileSystemAccessRule>()
                    .Select(g =>
                    {
                        return new SecurityGroupDto(
                            GetName(g.IdentityReference),
                            g.FileSystemRights.HasFlag(FileSystemRights.FullControl),
                            g.FileSystemRights.HasFlag(FileSystemRights.Modify),
                            g.FileSystemRights.HasFlag(FileSystemRights.ReadAndExecute),
                            g.FileSystemRights.HasFlag(FileSystemRights.ListDirectory),
                            g.FileSystemRights.HasFlag(FileSystemRights.Read),
                            g.FileSystemRights.HasFlag(FileSystemRights.Write)
                        );
                    })]);
            }

            public DirectoryFilesDto ToFilesDto()
                => new([.. di.EnumerateFiles().Select(fi => fi.ToSimpleDto())]);

            public DirectoryInfoSimpleDto ToInfoSimpleDto()
                => new(di.Name, di.FullName, di.LastWriteTimeUtc);

            public DirectoryDirectoriesDto ToDirectoriesDto()
                => new([.. di.EnumerateDirectories().Select(ToInfoSimpleDto)]);
        }

        static string GetName(IdentityReference id)
        {
            if (!OperatingSystem.IsWindows())
                throw new NotSupportedException("Server OS doesn't GetName operation");

            try { return id.Translate(typeof(NTAccount)).Value; }
            catch { return id.Value; }
        }
    }
}
