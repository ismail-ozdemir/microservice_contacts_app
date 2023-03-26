using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Api.Dto.ExceptionResponses
{
    [ExcludeFromCodeCoverage]
    public abstract class NotFound : ExceptionResponseDtoBase
    {
        public string Message { get; }
        protected NotFound(string message)
        {
            this.Message = message;
        }
        public class RecodExceptionResponseDto : NotFound
        {
            public RecodExceptionResponseDto(string message) : base(message)
            {

            }
        }
        public class FileExceptionResponseDto : NotFound
        {
            public FileExceptionResponseDto(string message) : base(message)
            {

            }
        }
    }

}
