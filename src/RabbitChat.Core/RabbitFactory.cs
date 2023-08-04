using RabbitMQ.Client;

namespace RabbitChat.Core;

public class RabbitFactory : IDisposable
{
    private IConnection? Connection { get; }

    public IModel? Channel { get; }

    public RabbitFactory(string hostName, int port, string userName, string password)
    {
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
    }

    public void Dispose()
    {
        Channel?.Dispose();
        Connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}