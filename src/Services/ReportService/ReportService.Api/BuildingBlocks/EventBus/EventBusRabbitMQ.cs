using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using BuildingBlocks.EventBus.Absractions;
using System.Net.Sockets;
using System.Text;

namespace BuildingBlocks.EventBus
{

    public class EventBusRabbitMQ : IEventBus
    {
        private readonly RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory? connectionFactory;
        private readonly IModel publishChannel;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        protected EventBusConfig _config { get; set; }


        public EventBusRabbitMQ(IServiceProvider serviceProvider, EventBusConfig config)
        {
            _config = config;
            _logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();

            if (config.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(config, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);


            }
            if (connectionFactory == null)
                connectionFactory = new ConnectionFactory();


            var loggerPersistent = serviceProvider.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            persistentConnection = new RabbitMQPersistentConnection(connectionFactory, loggerPersistent, config.ConnectionRetryCount);
            publishChannel = CreateChannel();

        }

        public void Publish(IQeueEvent @event)
        {
            var policy = Policy.Handle<SocketException>().Or<BrokerUnreachableException>()
                                 .WaitAndRetry(
                                 retryCount: _config.ConnectionRetryCount,
                                 sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                 onRetry: (ex, time, retry, ctx) =>
                                 {
                                     _logger.LogWarning(ex, "[{instance}] {event} Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                                                         nameof(EventBusRabbitMQ), nameof(EventBusRabbitMQ.Publish), ex.GetType().Name, ex.Message, retry, _config.ConnectionRetryCount);
                                 }
                    );

            string routingKey = @event.GetType().Name;
            string message = JsonConvert.SerializeObject(@event);
            byte[] data = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {

                var props = publishChannel.CreateBasicProperties();
                props.DeliveryMode = 2; // Persistent message
                publishChannel.BasicPublish(
                    exchange: _config.DefaultTopicName,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: props,
                    body: data
                    );
            });
        }


        private IModel CreateChannel()
        {
            if (!persistentConnection.isConnected)
                persistentConnection.TryConnect();

            var channel = persistentConnection.CreateChannel();
            channel.ExchangeDeclare(exchange: _config.DefaultTopicName, type: ExchangeType.Direct);

            return channel;
        }
    }
}
