using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using EstacionaCareca.Shared.DTOs;
using EstacionaCareca.Shared.Configuration;

namespace EstacionaCareca.Servico.Services;

public class RabbitMqPublisher
{
    private readonly ConnectionFactory _factory;

    public RabbitMqPublisher()
    {
        _factory = new ConnectionFactory
        {
            HostName = Configuration.RabbitMqHost,
            Port = Configuration.RabbitMqPort,
            UserName = Configuration.RabbitMqUser,
            Password = Configuration.RabbitMqPassword
        };
    }

    public Task PublishAsync(CameraMessageDto message)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        // Conexão curta para simplicidade; para produção prefira singleton/reuso de conexão e canal.
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: Configuration.RabbitMqQueue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: Configuration.RabbitMqQueue,
                             basicProperties: properties,
                             body: body);

        return Task.CompletedTask;
    }
}