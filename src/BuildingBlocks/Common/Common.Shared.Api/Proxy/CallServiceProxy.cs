using Common.Shared.Exceptions;
using Common.Shared.Api.Wrappers;
using System.Diagnostics;

namespace Common.Shared.Api.Proxy
{
    public static class CallServiceProxy
    {
        public static ApiResponse CallService(Action serviceMethod)
        {
            if (serviceMethod == null)
                throw new ArgumentNullException(nameof(serviceMethod));

            var serviceExecutionResult = ExecuteService(serviceMethod);
            return new ApiResponse
            {
                IsCompletedSuccessfully = true,
                ServiceInvokeDurationInTicks = serviceExecutionResult.ServiceCallDurationInTicks,
                ServiceInvokeDurationInMilliseconds = serviceExecutionResult.ServiceCallDurationInMilliseconds
            };
        }
        public static ApiResponse<TServiceResult> CallService<TServiceResult>(Func<TServiceResult> serviceMethod)
        {
            if (serviceMethod == null)
                throw new ArgumentNullException(nameof(serviceMethod));

            var serviceExecutionResult = ExecuteService(serviceMethod);
            return new ApiResponse<TServiceResult>
            {
                IsCompletedSuccessfully = true,
                ServiceInvokeDurationInTicks = serviceExecutionResult.ServiceCallDurationInTicks,
                ServiceInvokeDurationInMilliseconds = serviceExecutionResult.ServiceCallDurationInMilliseconds,
                Data = serviceExecutionResult.ServiceResult
            };
        }
        public static async Task<ApiResponse<TServiceResult>> CallServiceAsync<TServiceResult>(Func<Task<TServiceResult>> serviceMethod)
        {
            if (serviceMethod == null)
                throw new ArgumentNullException(nameof(serviceMethod));

            var serviceExecutionResult = await ExecuteServiceAsync(serviceMethod);
            return new ApiResponse<TServiceResult>
            {
                IsCompletedSuccessfully = true,
                ServiceInvokeDurationInTicks = serviceExecutionResult.ServiceCallDurationInTicks,
                ServiceInvokeDurationInMilliseconds = serviceExecutionResult.ServiceCallDurationInMilliseconds,
                Data = serviceExecutionResult.ServiceResult
            };
        }
        public static async Task<ApiResponse> CallServiceAsync(Func<Task> serviceMethod)
        {
            if (serviceMethod == null)
                throw new ArgumentNullException(nameof(serviceMethod));

            var serviceExecutionResult = await ExecuteServiceAsync(serviceMethod);
            return new ApiResponse
            {
                IsCompletedSuccessfully = true,
                ServiceInvokeDurationInTicks = serviceExecutionResult.ServiceCallDurationInTicks,
                ServiceInvokeDurationInMilliseconds = serviceExecutionResult.ServiceCallDurationInMilliseconds
            };
        }
        private static (long ServiceCallDurationInTicks, long ServiceCallDurationInMilliseconds) ExecuteService(Action serviceMethod)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                serviceMethod.Invoke();
                stopwatch.Stop();
                return (stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);
            }
            catch
            {
                stopwatch.Stop();
                throw;
            }
        }
        private static (TServiceResult ServiceResult, long ServiceCallDurationInTicks, long ServiceCallDurationInMilliseconds) ExecuteService<TServiceResult>(Func<TServiceResult> serviceMethod)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                var data = serviceMethod.Invoke();
                stopwatch.Stop();
                return (data, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);
            }
            catch
            {
                stopwatch.Stop();
                throw;
            }
        }
        private static async Task<(TServiceResult ServiceResult, long ServiceCallDurationInTicks, long ServiceCallDurationInMilliseconds)> ExecuteServiceAsync<TServiceResult>(Func<Task<TServiceResult>> serviceMethod)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                TServiceResult data = await serviceMethod.Invoke();
                stopwatch.Stop();
                return (data, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);
            }
            catch
            {
                stopwatch.Stop();
                throw;
            }
        }
        private static async Task<(long ServiceCallDurationInTicks, long ServiceCallDurationInMilliseconds)> ExecuteServiceAsync(Func<Task> serviceMethod)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                await serviceMethod.Invoke();
                stopwatch.Stop();
                return (stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                throw new CommonBaseException("An unhandled exception thrown during request handling. See inner exception for details.", ex)
                {
                    ServiceInvokeDurationInMilliseconds = stopwatch.ElapsedMilliseconds,
                    ServiceInvokeDurationInTicks = stopwatch.ElapsedTicks
                };
            }
        }
    }



}
