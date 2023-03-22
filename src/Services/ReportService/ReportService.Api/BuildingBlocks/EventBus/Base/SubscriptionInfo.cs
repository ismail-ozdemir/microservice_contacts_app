namespace BuildingBlocks.EventBus.Base
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; private set; }

        public SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }
        public static SubscriptionInfo Typed(Type handlerType) => new SubscriptionInfo(handlerType);
    }
}
