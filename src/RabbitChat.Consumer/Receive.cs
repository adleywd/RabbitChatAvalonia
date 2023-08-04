﻿using System.Text;
using RabbitChat.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitChat.Consumer;

public class Receive
{
    public EventHandler<BasicDeliverEventArgs> OnReceived { get; set; }

    private readonly RabbitFactory _rabbitFactory;
    
    public Receive()
    {
        _rabbitFactory ??= new RabbitFactory(
            AccessConstants.HostName, 
            AccessConstants.Port, 
            AccessConstants.UserName,
            AccessConstants.Password);
    }

    public void ReceiveMessage()
    {
        ArgumentNullException.ThrowIfNull(_rabbitFactory);
        ArgumentNullException.ThrowIfNull(_rabbitFactory.Channel);
        
        _rabbitFactory.Channel
            .QueueDeclare(
                queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        
        var consumer = new EventingBasicConsumer(_rabbitFactory.Channel);
        consumer.Received += OnReceived; 
        
        _rabbitFactory.Channel
            .BasicConsume(
                queue: "hello",
                autoAck: true,
                consumer: consumer);
        
    }
}