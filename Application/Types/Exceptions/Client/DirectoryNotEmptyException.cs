namespace Application.Types.Exceptions.Client
{
    internal class DirectoryNotEmptyException(string path) : BusinessException(400, "Директория не пуста", $"Directory is not empty: (path: {path})");
}
