namespace Application.Types.Exceptions.Client
{
    public class DirectoryDeletionForbiddenException(string path) : BusinessException(403, "Недостаточно прав для удаления директории", $"Directory deletion forbidden. (path: {path})");
}
