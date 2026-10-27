namespace Application.Types.Exceptions.Client
{
    internal class PathNotAbsoluteException(string path) : BusinessException(400,
        "Путь к директории должен быть абсолютным", 
        $"Directory path must be absolute. (path: {path})");
}
