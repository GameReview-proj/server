using GameReview.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GameReview.Infra.RabbitMq;

public class RabbitMqProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqProducer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
                queue: "notifications",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
        );
    }

    /// <summary>
    /// Método responsável pela publicação de notificações para o RabbitMQ. Sempre acabe a execução com o método Dispose() no fim da cadeia de métodos.
    /// </summary>
    /// <param name="notification">Notificação a ser publicada</param>
    public RabbitMqProducer PublishNotification(Notification notification)
    {
        var message = JsonSerializer.Serialize(notification);

        Publish(message, "notifications");

        return this;
    }

    public void Publish(string message, string routingKey)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", routingKey: routingKey, basicProperties: null, body: body);
    }

    public void Dispose()
    {
        _connection.Close();
        _channel.Close();
    }
}