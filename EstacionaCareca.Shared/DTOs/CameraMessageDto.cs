namespace EstacionaCareca.Shared.DTOs;

public class CameraMessageDto
{
    public int CameraId { get; set; }
    public string CaminhoImagem { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}