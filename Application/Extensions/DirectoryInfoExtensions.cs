using Application.Objects;

namespace Application.Extensions
{
    internal static class DirectoryInfoExtensions
    {
        extension (DirectoryInfo di)
        {
            public DirectoryTreeMeasurement Measure()
            {
                long length = 0, filesCount = 0, directoriesCount = 0;

                foreach (var file in di.EnumerateFiles())
                {
                    length += file.Length;
                    filesCount++;
                }

                foreach (var dir in di.EnumerateDirectories())
                {
                    if ((dir.Attributes & FileAttributes.ReparsePoint) != 0)  // ссылки скипаем
                        continue;

                    var measurement = dir.Measure();
                    length += measurement.Length;
                    filesCount += measurement.FilesCount;
                    directoriesCount += measurement.DirectoriesCount + 1; // +1 потому что сама директория идёт в учет тоже, она сама себя ведь не считает да? - да.
                }

                return new(length, filesCount, directoriesCount);
            }
        }
    }
}
