using Microsoft.Extensions.DependencyInjection;
using EventBus.Base;
using EventBus.RabbitMQ.Configurations;

namespace EventBus.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            IServiceCollection services = new ServiceCollection();
            IServiceProvider provider = null;
            
            services.RegisterEventBus(conf => conf.UseRabbitMQ(new(), provider));

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}