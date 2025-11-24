using EstacionaCareca.Shared.Models;

namespace EstacionaCareca.Shared.Configuration
{
    public static class CameraRegistro
    {
        public static List<Camera> Cameras = new List<Camera>
        {
            new Camera
            {
                Id = 1,
                Nome = "Camera Entrada 1",
                DiretorioImagens = @"C:\Cameras\Entrada1",
                Ativa = true
            },
            new Camera
            {
                Id = 2,
                Nome = "Camera Entrada 2",
                DiretorioImagens = @"C:\Cameras\Entrada2",
                Ativa = true
            },
            new Camera
            {
                Id = 3,
                Nome = "Camera Saída",
                DiretorioImagens = @"C:\Cameras\Saida",
                Ativa = true
            }
        };
    }
}
