using EstacionaCareca.Servico.Cameras;
using EstacionaCareca.Shared.Configuration;
using EstacionaCareca.Servico.Services;
using System.Diagnostics;
using System.Reflection;

class Program
{
    static async Task Main(string[] args)
    {
        // consumer -> inicia o consumidor que chama a API
        if (args.Length > 0 && args[0].Equals("consumer", StringComparison.OrdinalIgnoreCase))
        {
            var api = new ApiService();
            var consumer = new RabbitMqConsumer(api);

            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            await consumer.StartAsync(cts.Token);
            return;
        }

        // child: cameraId
        if (args.Length > 0 && int.TryParse(args[0], out var cameraId))
        {
            var cam = CameraRegistro.Cameras.FirstOrDefault(c => c.Id == cameraId && c.Ativa);
            if (cam == null)
            {
                Console.WriteLine($"Câmera com id {cameraId} não encontrada ou não está ativa.");
                return;
            }

            var runner = new CameraRunner(cam);
            await runner.StartAsync();
            return;
        }

        // launcher
        Console.WriteLine("Iniciando câmeras (modo launcher)...");

        var baseDir = AppContext.BaseDirectory ?? Directory.GetCurrentDirectory();
        var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name ?? "EstacionaCareca.Servico";
        var dllPath = Path.Combine(baseDir, $"{assemblyName}.dll");
        var exePath = Assembly.GetEntryAssembly()?.Location;

        foreach (var cam in CameraRegistro.Cameras.Where(c => c.Ativa))
        {
            try
            {
                string cmdArgs;
                if (File.Exists(dllPath))
                {
                    cmdArgs = $"/c start dotnet \"{dllPath}\" {cam.Id}";
                }
                else if (!string.IsNullOrWhiteSpace(exePath) && File.Exists(exePath))
                {
                    cmdArgs = $"/c start \"\" \"{exePath}\" {cam.Id}";
                }
                else
                {
                    Console.WriteLine("Não foi possível localizar o binário de execução (.dll ou .exe).");
                    continue;
                }

                var psi = new ProcessStartInfo("cmd.exe", cmdArgs)
                {
                    CreateNoWindow = false,
                    UseShellExecute = true
                };

                Process.Start(psi);
                Console.WriteLine($"Janela iniciada para: {cam.Nome} (id {cam.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar janela para {cam.Nome}: {ex.Message}");
            }
        }

        Console.WriteLine("Todas as janelas foram solicitadas. Feche-as antes de recompilar se necessário.");
        Console.WriteLine("Para iniciar o consumidor execute: dotnet run --project EstacionaCareca.Servico -- consumer");
    }
}
