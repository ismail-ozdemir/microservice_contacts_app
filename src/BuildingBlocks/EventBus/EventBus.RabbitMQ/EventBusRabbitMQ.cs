using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client.Events;
using System.Net.Sockets;
using System.Text;
using EventBus.Base.Concrete;
using Microsoft.Extensions.Logging;
using EventBus.Base.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using EventBus.RabbitMQ.Configurations;

namespace EventBus.RabbitMQ
{

    internal class EventBusRabbitMQ : BaseEventBus
    {
        private readonly RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory? connectionFactory;
        private readonly IModel publishChannel;
        private readonly IModel consumerChannel;
        private readonly new ILogger<EventBusRabbitMQ> _logger;

        protected RabbitMqConfig _config { get; set; }
        public EventBusRabbitMQ(IServiceProvider serviceProvider, RabbitMqConfig config) : base(serviceProvider)
        {
            _config = config;
            _logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            if (config.Connection != null)
            {
                // TODO : düzenlenecek
                string connJson = JsonConvert.SerializeObject(config, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);


            }
            if (connectionFactory == null)
                connectionFactory = new ConnectionFactory();


            var loggerPersistent = serviceProvider.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            persistentConnection = new RabbitMQPersistentConnection(connectionFactory, loggerPersistent, config.ConnectionRetryCount);
            publishChannel = CreateChannel();
            consumerChannel = CreateChannel();
            if (config.ConsumerMessageLimit > 0)
            {
                _logger.LogInformation("{Application} Prefetch Count: {prefetchCount}", config.SubscriberClientAppName, config.ConsumerMessageLimit);
                consumerChannel.BasicQos(
                     prefetchSize: 0,
                     prefetchCount: config.ConsumerMessageLimit,
                     global: false
                    );

            }
        }

        public override void Publish(IEvent @event)
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

                IBasicProperties props = publishChannel.CreateBasicProperties();
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



        public override void Subscribe<TEvent, TEventHandler>()
        {
            string eventName = typeof(TEvent).Name;
            if (!SubsManager.HasSubcriptionForEvent(eventName))
            {
                if (!persistentConnection.isConnected)
                    persistentConnection.TryConnect();

                consumerChannel.QueueDeclare(
                       queue: GetSubscriberQueueName(eventName),
                       durable: true,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null
                       );

                consumerChannel.QueueBind(
                    queue: GetSubscriberQueueName(eventName),
                    exchange: _config.DefaultTopicName,
                    routingKey: eventName
                    );
            }
            SubsManager.AddSubscription<TEvent, TEventHandler>();
            StartBasicConsume(eventName);

        }

        private IModel CreateChannel()
        {
            if (!persistentConnection.isConnected)
                persistentConnection.TryConnect();

            var channel = persistentConnection.CreateChannel();
            channel.ExchangeDeclare(exchange: _config.DefaultTopicName, type: ExchangeType.Direct);

            return channel;
        }


        private string GetSubscriberQueueName(string eventName) => $"{_config.SubscriberClientAppName}.{eventName}";
        private void StartBasicConsume(string eventName)
        {
            if (consumerChannel != null)
            {
                EventingBasicConsumer consumer = new EventingBasicConsumer(consumerChannel);
                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(
                    queue: GetSubscriberQueueName(eventName),
                    autoAck: false,
                    consumer: consumer
                    );
            }
        }
        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {

            string eventName = e.RoutingKey;
            string message = Encoding.UTF8.GetString(e.Body.Span);
            try
            {
                // TODO : await olmamalı
                await ProcessEvent(eventName, message, (isSuccess) =>
                {
                    if (isSuccess)
                        consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
                    else
                        consumerChannel.BasicNack(e.DeliveryTag, multiple: false, requeue: true); //TODO :  başka bir kuyruğa eklemesi gerekli.
                });

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "eventhandler not triggered");
                consumerChannel.BasicNack(e.DeliveryTag, multiple: false, requeue: false); //TODO :  başka bir kuyruğa eklemesi gerekli.
            }
        }
    }
}
