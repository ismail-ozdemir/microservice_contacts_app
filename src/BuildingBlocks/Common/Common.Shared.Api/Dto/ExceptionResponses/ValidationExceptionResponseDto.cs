using Common.Shared.Exceptions;

namespace Common.Shared.Api.Dto.ExceptionResponses
{
    public abstract class ExceptionResponseDtoBase
    {
        public string TypeName => this.GetType().Name;
    }
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
