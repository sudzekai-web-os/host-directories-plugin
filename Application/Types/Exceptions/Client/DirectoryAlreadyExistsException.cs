namespace Application.Types.Exceptions.Client
{
    public class DirectoryAlreadyExistsException(string path) : BusinessException(409, "Директория уже существует", $"Directory already exists. (path: {path})");
}
