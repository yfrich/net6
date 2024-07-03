using System.Reflection.Metadata.Ecma335;
using Zack.EventBus;

namespace EventWebApi2
{
    //可以监听多个消息
    [EventName("OrderCreated")]
    //[EventName("OrderCreated2")]
    public class EventHandler3 : JsonIntegrationEventHandler<OrderData>
    {
        public override Task HandleJson(string eventName, OrderData? eventData)
        {
            Console.WriteLine($"我是微服务2的EventHandler3,收到了订单：{eventData}");
            return Task.CompletedTask;
        }
    }
    public record OrderData(long Id, string Name, DateTime Date);

}
