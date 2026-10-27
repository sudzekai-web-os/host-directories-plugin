using Application.Types.Exceptions.Client;

namespace Application.Services
{
    internal class DirectoryPathValidator
    {
        public void Validate(string path)
        {
            ThrowIfNotFullyQualified(path);
            ThrowIfNotExists(path);
        }

        public void ThrowIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                throw new Types.Exceptions.Client.DirectoryNotFoundException(path);
        }

        public void ThrowIfNotFullyQualified(string path)
        {
            if (!Path.IsPathFullyQualified(path))
                throw new PathNotAbsoluteException(path);
        }

        public void ThrowIfExists(string path)
        {
            if (Directory.Exists(path))
                throw new DirectoryAlreadyExistsException(path);
        }
    }
}
