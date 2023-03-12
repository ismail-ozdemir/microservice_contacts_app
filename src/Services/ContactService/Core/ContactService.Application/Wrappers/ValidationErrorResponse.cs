using ContactService.Application.Dto;

namespace ContactService.Application.Wrappers
{
    public sealed class ValidationErrorResponse : ServiceResponse<ModelValidationErrorResponse>
    {

        public ValidationErrorResponse(ModelValidationErrorResponse value) : base(value)
        {
            isSuccess = false;
        }
    }
}
