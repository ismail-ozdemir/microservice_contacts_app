﻿using Bogus;
using ContactService.Domain;
using ContactService.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

using Person = ContactService.Domain.Entities.Person;

namespace ContactService.Persistence.Context
{
    internal class ContactContextSeed
    {
        internal async Task SeedAsync(ContactsContext context, ILogger<ContactContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ContactContextSeed));
            await policy.ExecuteAsync(() => ProccessSeeding(context, logger));
        }

        private static async Task ProccessSeeding(ContactsContext context, ILogger<ContactContextSeed> logger)
        {

            if (!context.Persons.Any())
            {
                logger.LogInformation("Fake data generating");
                var data = GetFakeData();
                var users = data.Item1;
                var info = data.Item2;
                logger.LogInformation("Fake data generated");




                await context.Persons.AddRangeAsync(users);
                await context.SaveChangesAsync();
                logger.LogInformation("{SeedData} seed data saved. Record Count :{RecordCount}", "Users", users.Count);

                await context.ContactInformations.AddRangeAsync(info);
                await context.SaveChangesAsync();
                logger.LogInformation("{SeedData} seed data saved. Record Count :{RecordCount}", "ContactInformations", info.Count);



            }

        }

        private static AsyncRetryPolicy CreatePolicy(ILogger<ContactContextSeed> logger, string prefix, int retries = 5)
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


        private static Tuple<List<Person>, List<ContactInformation>> GetFakeData()
        {

            List<ContactInformation> info = new();

            var userFaker = new Faker<Person>("tr");

            userFaker.RuleFor(u => u.Id, f => f.Random.Uuid())
                     .RuleFor(u => u.Name, f => f.Person.FirstName)
                     .RuleFor(u => u.Surname, f => f.Person.LastName)
                     .RuleFor(u => u.Company, f => f.Company.Bs())
                     .FinishWith((f, u) =>
                     {

                         info.Add(new() { Id = Guid.NewGuid(), PersonId = u.Id, InformationType = InformationType.Email, Content = f.Person.Email });
                         info.Add(new() { Id = Guid.NewGuid(), PersonId = u.Id, InformationType = InformationType.Location, Content = f.Address.City() });
                         info.Add(new() { Id = Guid.NewGuid(), PersonId = u.Id, InformationType = InformationType.Phone, Content = f.Person.Phone });
                     });

            var users = userFaker.Generate(100);
            return Tuple.Create(users, info);
        }



    }

}
