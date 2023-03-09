﻿using Polly;
using Polly.Retry;
using Microsoft.Data.SqlClient;
using ContactService.Api.Data.Models;
using Bogus;

namespace ContactService.Api.Data.Context
{

    public class ContactContextSeed
    {
        public async Task SeedAsync(ContactsContext context, ILogger<ContactContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ContactContextSeed));

            await policy.ExecuteAsync(() => ProccessSeeding(context, logger));


        }

        private async Task ProccessSeeding(ContactsContext context, ILogger<ContactContextSeed> logger)
        {

            if (!context.Users.Any())
            {
                logger.LogInformation("Fake data generating");
                var data = GetFakeData(logger);
                var users = data.Item1;
                var info = data.Item2;
                logger.LogInformation("Fake data generated");




                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
                logger.LogInformation("{SeedData} seed data saved","Users");
                
                await context.ContactInformations.AddRangeAsync(info);
                await context.SaveChangesAsync();
                logger.LogInformation("{SeedData} seed data", "ContactInformations");
                
            }

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




        private Tuple<List<User>, List<ContactInformation>> GetFakeData(ILogger<ContactContextSeed> logger)
        {

            List<ContactInformation> info = new();

            var userFaker = new Faker<User>("tr");

            userFaker.RuleFor(u => u.Id, f => f.Random.Uuid())
                     .RuleFor(u => u.Name, f => f.Person.FirstName)
                     .RuleFor(u => u.Surname, f => f.Person.LastName)
                     .RuleFor(u => u.Company, f => f.Company.Bs())
                     .FinishWith((f, u) =>
                     {

                         info.Add(new() { Id = Guid.NewGuid(), UserId = u.Id, InformationType = InformationType.Email, Content = f.Person.Email });
                         info.Add(new() { Id = Guid.NewGuid(), UserId = u.Id, InformationType = InformationType.Location, Content = f.Address.City() });
                         info.Add(new() { Id = Guid.NewGuid(), UserId = u.Id, InformationType = InformationType.Phone, Content = f.Person.Phone });
                     });

            var users = userFaker.Generate(100);
            return Tuple.Create(users, info);
        }



    }

}
