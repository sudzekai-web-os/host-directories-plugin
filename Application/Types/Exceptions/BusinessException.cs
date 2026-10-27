namespace Application.Types.Exceptions
{
    public class BusinessException(int code, string businessMessage, string message) : Exception(message)
    {
        public int StatusCode { get; } = code;

        public string BusinessMessage { get; } = businessMessage;
    }
}
