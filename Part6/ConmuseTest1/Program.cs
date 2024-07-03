// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.HostName = "127.0.0.1";
factory.DispatchConsumersAsync = true;
var connection = factory.CreateConnection();//连接TCP
string exchangeName = "exchange1";
string queueName = "queue1";
string routingKey = "key1";
using var channel = connection.CreateModel();//创建一个虚拟信道
channel.ExchangeDeclare(exchangeName, "direct");//声明交换机
channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);//创建一个队列
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);//设置该队列接收哪个交换机的哪个routingkey 的消息

var consumer = new AsyncEventingBasicConsumer(channel);//创建消息消费者(拉取者) 从队列中异步获取消息
consumer.Received += Consumer_Received;

channel.BasicConsume(queue: queueName, false, consumer: consumer);//队列绑定消费者进行监听是否有消息进入队列， 进入队列直接调用消费者进行消费。

Console.WriteLine("按回车键退出");
Console.ReadLine();
async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
{
    try
    {
        var bytes = args.Body.ToArray();
        string msg = Encoding.UTF8.GetString(bytes);
        Console.WriteLine($"{DateTime.Now}:收到了消息【{msg}】");
        //args.DeliveryTag  这个就是消息的编号
        channel.BasicAck(args.DeliveryTag, multiple: false);
        await Task.Delay(1000);
    }
    catch (Exception ex)
    {
        channel.BasicReject(args.DeliveryTag, true);//失败重发
        Console.WriteLine($"处理收到的消息出错:{ex}");
    }
}