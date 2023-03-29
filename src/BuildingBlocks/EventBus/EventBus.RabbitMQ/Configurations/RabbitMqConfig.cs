
namespace EventBus.RabbitMQ.Configurations
{
    public class RabbitMqConfig
    {
        public int ConnectionRetryCount { get; set; } = 5;
        public string DefaultTopicName { get; set; } = "EventBus";
        public string ConnectionString { get; set; } = string.Empty;
        public string SubscriberClientAppName { get; set; } = string.Empty;
        public object? Connection { get; set; }
        public ushort ConsumerMessageLimit { get; set; } = 0;
        public string EventNamePrefix { get; set; } = string.Empty;
        public string EventNameSuffix { get; set; } = string.Empty;
    }
}
