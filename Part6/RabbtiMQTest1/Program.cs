// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

var connFactory = new ConnectionFactory();
connFactory.HostName = "127.0.0.1";
connFactory.DispatchConsumersAsync = true;
//TCP连接
var connection = connFactory.CreateConnection();
string exchangeName = "exchange1";
while (true)
{
    using var channel = connection.CreateModel();//虚拟信道
    var prop = channel.CreateBasicProperties();
    prop.DeliveryMode = 2;//1 非持久化 2 持久化
    channel.ExchangeDeclare(exchangeName, "direct");//声明交换机
    byte[] bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString());//构建发送数据
    channel.BasicPublish(exchangeName, routingKey: "key1", mandatory: true, basicProperties: prop, body: bytes);//发送消息
    Console.WriteLine($"ok:{DateTime.Now}");
    Thread.Sleep(1000);
}

