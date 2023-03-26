using Common.Shared.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Api.Dto.ExceptionResponses
{
    [ExcludeFromCodeCoverage]
    public class ValidationExceptionResponseDto : ExceptionResponseDtoBase
    {
        public string Message { get; }
        public List<ValidationException.ResponseItem> Errors { get; }
        public ValidationExceptionResponseDto(List<ValidationException.ResponseItem> errors, string message = "")
        {
            this.Errors = errors;
            this.Message = message;
        }

    }

}
