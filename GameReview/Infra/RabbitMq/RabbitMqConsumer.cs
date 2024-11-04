using GameReview.Infra.SignalR;
using GameReview.Models;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace GameReview.Infra.RabbitMq;

public class RabbitMqConsumer
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public RabbitMqConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = _hubContext;

        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var notificationString = Encoding.UTF8.GetString(body);
            var notification = JsonSerializer.Deserialize<Notification>(notificationString);

            await _hubContext.Clients.Group(notification.UserId).SendAsync("ReceiveNotification", notification);
        };
        
        channel.BasicConsume(queue: "notifications", autoAck: true, consumer: consumer);
    }
}