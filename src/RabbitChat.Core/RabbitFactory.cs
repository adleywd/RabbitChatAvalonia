using RabbitMQ.Client;

namespace RabbitChat.Core;

public class RabbitFactory : IDisposable
{
    public string QueueName { get; } = "hello";
    public string RoutingKey { get; } = "hello";

    private static RabbitFactory? _instance;

    private IConnection? Connection { get; }

    public IModel? Channel { get; }

    public static RabbitFactory GetInstance(string hostName, int port, string userName, string password)
    {
        return _instance ?? new RabbitFactory(hostName, port, userName, password);
    }
    
    private RabbitFactory(string hostName, int port, string userName, string password)
    {
        if (_instance != null)
        {
            return;
        }
        
        if (Connection != null)
        {
            return;
        }

        if (Channel != null)
        {
            return;
        }

        var factory = new ConnectionFactory
            { HostName = hostName, Port = port, UserName = userName, Password = password };
        
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
        _instance = this;
    }

    public void Dispose()
    {
        Channel?.Dispose();
        Connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}