using Zack.EventBus;

namespace EventWebApi2
{
    //可以监听多个消息
    [EventName("OrderCreated")]
    //[EventName("OrderCreated2")]
    public class EventHandler2 : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            if (eventName == "OrderCreated")
            {
                Console.WriteLine($"我是微服务2的EventHandler2收到了订单,eventData={eventData}");
            }
            return Task.CompletedTask;
        }
    }
}
