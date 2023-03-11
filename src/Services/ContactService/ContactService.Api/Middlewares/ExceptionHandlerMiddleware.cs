using ContactService.Application.Dto;
using System.Net;

namespace ContactService.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public RequestDelegate Next { get; }
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await Next.Invoke(httpContext);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new FieldError { Field = e.PropertyName, Value = e.AttemptedValue, Error = e.ErrorMessage }).ToList();
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Application.Wrappers.ValidationErrorResponse response = new(new(errors, ex.Message));
                await httpContext.Response.WriteAsJsonAsync(response);

            }
        }


    }
}
