using ContactService.Application.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Application.Wrappers
{
    public sealed class ValidationErrorResponse : ServiceResponse<ModelValidationErrorResponse>
    {

        public ValidationErrorResponse(Dto.ModelValidationErrorResponse value) : base(value)
        {
            isSuccess = false;
        }
    }
}
