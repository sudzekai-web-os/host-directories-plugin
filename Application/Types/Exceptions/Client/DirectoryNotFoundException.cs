namespace Application.Types.Exceptions.Client
{
    public class DirectoryNotFoundException(string path) : BusinessException(404,
        "Указанная директория не найдена",
        $"Directory not found. (path: {path})");
}
