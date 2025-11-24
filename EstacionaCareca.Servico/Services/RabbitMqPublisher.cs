using EstacionaCareca.Shared.Configuration;
using EstacionaCareca.Shared.DTOs;
using RabbitMQ.Client;
using System.Text.Json;

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

    public async Task<Task> PublishAsync(CameraMessageDto message)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        // Conexão curta para simplicidade; para produção prefira singleton/reuso de conexão e canal.
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: Configuration.RabbitMqQueue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(exchange: string.Empty,
                             routingKey: Configuration.RabbitMqQueue,
                             mandatory: false,
                             basicProperties: properties,
                             body: body);

        return Task.CompletedTask;
    }
}