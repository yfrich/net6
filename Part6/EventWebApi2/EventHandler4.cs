using System.Reflection.Metadata.Ecma335;
using Zack.EventBus;

namespace EventWebApi2
{
    //可以监听多个消息
    [EventName("OrderCreated")]
    //[EventName("OrderCreated2")]
    public class EventHandler4 : DynamicIntegrationEventHandler
    {
        public override Task HandleDynamic(string eventName, dynamic eventData)
        {
            Console.WriteLine($"我是微服务2的EventHandler4,收到了订单：{eventData.Id},{eventData.Name}");
            return Task.CompletedTask;
        }
    }
}
