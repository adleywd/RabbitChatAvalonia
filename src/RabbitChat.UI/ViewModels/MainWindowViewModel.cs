using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RabbitChat.Consumer;
using RabbitChat.Producer;
using RabbitMQ.Client.Events;

namespace RabbitChat.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _messageToSend = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<string> _messagesReceived = new();

    private readonly Send _rabbitChatProducer;
    
    private readonly Receive _rabbitChatConsumer;

    public MainWindowViewModel()
    {
        _rabbitChatProducer = new Send();
        _rabbitChatConsumer = new Receive();
        _rabbitChatConsumer.OnReceived += OnMessageReceived;
        _rabbitChatConsumer.ReceiveMessage();
    }

    private void OnMessageReceived(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        MessagesReceived.Add(message);
        Console.WriteLine("Received {0}", message);
    }

    [RelayCommand]
    private void SendMessage()
    {
        _rabbitChatProducer.SendMessage(MessageToSend);
    }
    
}