using EstacionaCareca.Shared.Models;
using EstacionaCareca.Shared.DTOs;
using EstacionaCareca.Servico.Services;

namespace EstacionaCareca.Servico.Cameras
{
    public class CameraRunner
    {
        private readonly Camera _camera;
        private readonly RabbitMqPublisher _publisher;

        public CameraRunner(Camera camera)
        {
            _camera = camera;
            _publisher = new RabbitMqPublisher();
        }

        public async Task StartAsync()
        {
            Console.Title = _camera.Nome;
            Console.WriteLine($"*** {_camera.Nome} INICIADA ***");
            Console.WriteLine("Digite o caminho da imagem da placa:");
            Console.WriteLine("-----------------------------------");

            while (true)
            {
                Console.Write("Imagem: ");
                string? caminho = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(caminho))
                    continue;

                if (!File.Exists(caminho))
                {
                    Console.WriteLine("Arquivo não encontrado. Tente novamente.");
                    continue;
                }

                Console.WriteLine("Enviando mensagem para a fila...");

                var msg = new CameraMessageDto
                {
                    CameraId = _camera.Id,
                    CaminhoImagem = caminho,
                    Timestamp = DateTime.UtcNow
                };

                try
                {
                    await _publisher.PublishAsync(msg);
                    Console.WriteLine("Mensagem publicada na fila com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao publicar na fila: {ex.Message}");
                }

                Console.WriteLine("-----------------------------------");
            }
        }
    }
}
