using Polly;
using Polly.Retry;
using Microsoft.Data.SqlClient;


namespace ContactService.Api.Data.Context
{

    public class ContactContextSeed
    {
        public async Task SeedAsync(ContactsContext context, ILogger<ContactContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ContactContextSeed));

            await policy.ExecuteAsync(() => ProccessSeeding(context, logger));


        }

        private async Task ProccessSeeding(ContactsContext context, ILogger logger)
        {


        }



        private AsyncRetryPolicy CreatePolicy(ILogger<ContactContextSeed> logger, string prefix, int retries = 5)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }



    }

}
