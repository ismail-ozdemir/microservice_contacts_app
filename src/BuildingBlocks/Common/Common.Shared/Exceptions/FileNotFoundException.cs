
namespace Common.Shared.Exceptions
{
    public class FileNotFoundException : CommonExceptionBase
    {
        public FileNotFoundException(string message) : base(message)
        {
        }

        public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
