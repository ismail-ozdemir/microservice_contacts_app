using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        private IConnection connection { get; set; }
        private readonly IConnectionFactory connectionFactory;
        private readonly object lock_object = new();
        private readonly int retryCount;
        private bool _disposed;
        private readonly ILogger<RabbitMQPersistentConnection> _logger;
        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQPersistentConnection> logger, int retryCount = 5)
        {
            _logger = logger;
            this.retryCount = retryCount;
            this.connectionFactory = connectionFactory;
            TryConnect();

        }

        public bool isConnected => connection != null && connection.IsOpen;

        public IModel CreateChannel() => connection.CreateModel();

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>().Or<BrokerUnreachableException>()
                                 .WaitAndRetry(
                                    retryCount: retryCount,
                                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                    onRetry: (ex, time, retry, ctx) =>
                                 {

                                     _logger.LogWarning(ex, "[{instance}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                                                         nameof(RabbitMQPersistentConnection), ex.GetType().Name, ex.Message, retry, retryCount);
                                 }
                );
                policy.Execute(() =>
                {
                    connection = connectionFactory.CreateConnection();
                });

                if (isConnected)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    connection.CallbackException += Connection_CallbackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;
                    _logger.LogInformation("{instance} connected succesfully. HostName : {HostName}", nameof(RabbitMQPersistentConnection), connection.Endpoint.HostName);
                    return true;
                }
                return false;

            }
        }
        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {

            _logger.LogWarning("{instance} {event}. HostName : {HostName}", nameof(RabbitMQPersistentConnection), nameof(RabbitMQPersistentConnection.Connection_ConnectionShutdown), connection.Endpoint.HostName);
            if (_disposed) return;
            TryConnect();
        }
        private void Connection_CallbackException(object? sender, global::RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            _logger.LogWarning("{instance} {event}. Event Args: {ConnactionCallback} HostName : {HostName}", nameof(RabbitMQPersistentConnection), nameof(RabbitMQPersistentConnection.Connection_CallbackException), e, connection.Endpoint.HostName);
            if (_disposed) return;
            TryConnect();

        }
        private void Connection_ConnectionBlocked(object? sender, global::RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            _logger.LogWarning("{instance} {event}. Event Args : {ConnectionBlocked} HostName : {HostName}", nameof(RabbitMQPersistentConnection), nameof(RabbitMQPersistentConnection.Connection_ConnectionBlocked), e, connection.Endpoint.HostName);
            if (_disposed) return;
            TryConnect();
        }

        public void Dispose()
        {
            _logger.LogInformation("{instance} connaction disposed .HostName : {HostName}", nameof(RabbitMQPersistentConnection), connection.Endpoint.HostName);
            _disposed = true;
            connection.Dispose();
        }

    }
}
