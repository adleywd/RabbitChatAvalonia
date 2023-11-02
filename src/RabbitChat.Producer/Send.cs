using System.Text;
using RabbitChat.Core;
using RabbitMQ.Client;

namespace RabbitChat.Producer;

public class Send
{
    private readonly RabbitFactory _rabbitFactory;

    public Send()
    {
        _rabbitFactory ??= RabbitFactory.GetInstance(
            RabbitConstants.HostName, 
            RabbitConstants.Port, 
            RabbitConstants.UserName,
            RabbitConstants.Password);
    }

    public void SendMessage(string message)
    {
        ArgumentNullException.ThrowIfNull(_rabbitFactory);
        ArgumentNullException.ThrowIfNull(_rabbitFactory.Channel);
        
        _rabbitFactory.Channel.QueueDeclare(queue: _rabbitFactory.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        
        _rabbitFactory.Channel.ExchangeDeclare(RabbitConstants.TestExchangeName, ExchangeType.Direct, true);
        _rabbitFactory.Channel.QueueBind(_rabbitFactory.QueueName, RabbitConstants.TestExchangeName, _rabbitFactory.RoutingKey);
        
        var body = Encoding.UTF8.GetBytes(message);
        
        _rabbitFactory.Channel.BasicPublish(exchange: RabbitConstants.TestExchangeName,
            routingKey: _rabbitFactory.RoutingKey,
            basicProperties: null,
            body: body);
        
        Console.WriteLine("Sent {0}", message);
    }
}