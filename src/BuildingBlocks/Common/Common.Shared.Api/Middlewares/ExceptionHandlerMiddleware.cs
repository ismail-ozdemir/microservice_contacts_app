using System.Net;
using Microsoft.AspNetCore.Http;
using Common.Shared.Exceptions;
using Common.Shared.Api.Wrappers;
using Newtonsoft.Json;
using Common.Shared.Api.Dto.ExceptionResponses;

namespace Common.Shared.Api.Middlewares
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

            catch (Exception e)
            {
                switch (e)
                {
                    case ValidationException ex:
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            var result = new ApiResponse<ValidationExceptionResponseDto>()
                            {
                                Data = new(ex.Errors, ex.Message),
                                IsCompletedSuccessfully = false,
                                ServiceInvokeDurationInMilliseconds = ex.ServiceInvokeDurationInMilliseconds,
                                ServiceInvokeDurationInTicks = ex.ServiceInvokeDurationInTicks,
                            };
                            var json = JsonConvert.SerializeObject(result);
                            await httpContext.Response.WriteAsync(json);
                        }
                        break;
                    case RecordNotFoundException ex:
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            var result = new ApiResponse<NotFound.RecodExceptionResponseDto>()
                            {
                                Data = new(ex.Message),
                                IsCompletedSuccessfully = false,
                                ServiceInvokeDurationInMilliseconds = ex.ServiceInvokeDurationInMilliseconds,
                                ServiceInvokeDurationInTicks = ex.ServiceInvokeDurationInTicks,
                            };
                            var json = JsonConvert.SerializeObject(result);
                            await httpContext.Response.WriteAsync(json);
                        }
                        break;
                    case Exceptions.FileNotFoundException ex:
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            var result = new ApiResponse<NotFound.FileExceptionResponseDto>()
                            {
                                Data = new(ex.Message),
                                IsCompletedSuccessfully = false,
                                ServiceInvokeDurationInMilliseconds = ex.ServiceInvokeDurationInMilliseconds,
                                ServiceInvokeDurationInTicks = ex.ServiceInvokeDurationInTicks,
                            };
                            var json = JsonConvert.SerializeObject(result);
                            await httpContext.Response.WriteAsync(json);
                        }
                        break;
                    default:
                        throw;
                }
            }
        }
    }
}
