using GameReview.DTOs.Notification;
using GameReview.Infra.SignalR;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GameReview.Infra.RabbitMq;

public class RabbitMqConsumer
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;

        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var notificationString = Encoding.UTF8.GetString(body);
            var notification = JsonSerializer.Deserialize<OutNotificationDTO>(notificationString);

            await _hubContext.Clients.Group(notification.User.Id).SendAsync("ReceiveNotification", notification);
        };

        _channel.BasicConsume(queue: "notifications", autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _connection?.Close();
        _channel?.Close();
    }
}
