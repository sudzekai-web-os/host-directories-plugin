using Application.Dto.File;

namespace Application.Extensions
{
    internal static class FileInfoMappingExtensions
    {
        extension(FileInfo fi)
        {
            public FileInfoSimpleDto ToSimpleDto()
                => new(
                    fi.Name,
                    fi.FullName,
                    fi.Extension,
                    fi.Length,
                    fi.CreationTimeUtc,
                    fi.LastWriteTimeUtc);
        }
    }
}
