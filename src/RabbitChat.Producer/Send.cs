using System.Text;
using RabbitChat.Core;
using RabbitMQ.Client;

namespace RabbitChat.Producer;

public class Send
{
    private readonly RabbitFactory _rabbitFactory;
    public Send()
    {
        _rabbitFactory = new RabbitFactory(AccessConstants.HostName, AccessConstants.Port, AccessConstants.UserName, AccessConstants.Password);
    }
    
    public void SendMessage(string message)
    {
        _rabbitFactory.Channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        Console.WriteLine(" Press [enter] to exit or send type anything to send as a message.");
        
        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        
        var body = Encoding.UTF8.GetBytes(message);
        
        _rabbitFactory.Channel.BasicPublish(exchange: "",
            routingKey: "hello",
            basicProperties: null,
            body: body);
        
        Console.WriteLine(" [x] Sent {0}", message);
    }
}