using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EstacionaCareca.Shared.DTOs;
using EstacionaCareca.Shared.Configuration;

namespace EstacionaCareca.Servico.Services;

public class RabbitMqConsumer
{
    private readonly ConnectionFactory _factory;
    private readonly ApiService _api;

    public RabbitMqConsumer(ApiService api)
    {
        _api = api;
        _factory = new ConnectionFactory
        {
            HostName = Configuration.RabbitMqHost,
            Port = Configuration.RabbitMqPort,
            UserName = Configuration.RabbitMqUser,
            Password = Configuration.RabbitMqPassword
        };
    }

    public async Task<Task<object?>> StartAsync(CancellationToken cancellationToken)
    {
        var connection = await _factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: Configuration.RabbitMqQueue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

        // Process one message at a time per consumer
        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var msg = JsonSerializer.Deserialize<CameraMessageDto>(body);

                if (msg == null)
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    return;
                }

                Console.WriteLine($"[Consumer] Recebido mensagem da câmera {msg.CameraId} -> {msg.CaminhoImagem}");

                // Chama a API (pode lançar exceção)
                try
                {
                    var response = await _api.GetPlate(new RequestDto { CaminhoImagem = msg.CaminhoImagem });
                    var placa = response?.Resultados?.FirstOrDefault()?.Placa ?? "NÃO IDENTIFICADA";
                    Console.WriteLine($"[Consumer] Placa: {placa} (câmera {msg.CameraId})");
                }
                catch (Exception exApi)
                {
                    Console.WriteLine($"[Consumer] Erro ao chamar API: {exApi.Message}");
                    // Decide política: aqui fazemos ack para evitar retry infinito. Para retry, usar Nack e requeue=true com contador.
                }

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Consumer] Erro genérico: {ex.Message}");
                try { await channel.BasicAckAsync(ea.DeliveryTag, false); } catch { }
            }
        };

        await channel.BasicConsumeAsync(queue: Configuration.RabbitMqQueue,
                             autoAck: false,
                             consumer: consumer);

        // Mantém até cancelamento
        var tcs = new TaskCompletionSource<object?>();
        cancellationToken.Register(() =>
        {
            try
            {
                channel.CloseAsync();
                connection.CloseAsync();
            }
            catch { }
            tcs.TrySetResult(null);
        });

        Console.WriteLine("[Consumer] Iniciado e aguardando mensagens...");
        return tcs.Task;
    }
}