namespace EstacionaCareca.Shared.Models
{
    public class Camera
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string DiretorioImagens { get; set; } = string.Empty;
        public bool Ativa { get; set; } = true;
        public bool Coordenador { get; set; }
    }
}
