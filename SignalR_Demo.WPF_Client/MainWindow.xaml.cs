using System;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using SignalR_Demo.Names;

namespace SignalR_Demo.WPF_Client;

public partial class MainWindow : Window
{
    private readonly HubConnection _connection;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += Window_Loaded;

        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7141/chat")
            .Build();

        _connection.On<string>(SignalRNames.Receive,
            str =>
            {
                Dispatcher.Invoke(() =>
                {
                    ChatBox.Items.Insert(0, str);
                });
            });

        _connection.On<string>(SignalRNames.Notify,
            msg =>
            {
                MessageBox.Show(msg);
            });
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await _connection.StartAsync();
            ChatBox.Items.Add("Вы вошли в чат");

            ButtonSend.IsEnabled = true;
        }
        catch (Exception ex)
        {
            ChatBox.Items.Add(ex.Message);
        }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (InputUserName.Text == "0") await _connection.InvokeAsync(SignalRNames.ServerBroadcast);

            await _connection.InvokeAsync(SignalRNames.ServerSend, $"{InputUserName.Text}: {InputMessage.Text}");
        }
        catch (Exception ex)
        {
            ChatBox.Items.Add(ex.Message);
        }
    }
}