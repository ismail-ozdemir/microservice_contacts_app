namespace BuildingBlocks.EventBus.Absractions
{
    public interface IEventBus
    {
        public void Publish(IQeueEvent @event);
    }
}
