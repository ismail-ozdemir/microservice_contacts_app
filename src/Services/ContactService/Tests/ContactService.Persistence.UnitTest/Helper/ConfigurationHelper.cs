using ContactService.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Persistence.UnitTest.Helper
{
    internal static class ConfigurationHelper
    {
        public static ContactsContext GetFakeContactDbContext()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseInMemoryDatabase("MyInMemoryDatabseName")
                   .UseInternalServiceProvider(serviceProvider);

            var context = new ContactsContext(builder.Options);
            return context;

        }
    }
}
