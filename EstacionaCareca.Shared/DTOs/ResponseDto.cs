using System.ComponentModel;
using System.Text.Json.Serialization;

namespace EstacionaCareca.Shared.DTOs;

public class ResponseDto
{
    [JsonPropertyName("processing_time")]
    public decimal TempoProcessamento { get; set; }
    [JsonPropertyName("results")]
    public List<ResultadoDto> Resultados { get; set; } = new();
    [JsonPropertyName("filename")]
    public string NomeArquivo { get; set; } = string.Empty;
    [JsonPropertyName("version")]
    public int Versao { get; set; }
    [JsonPropertyName("camera_id")]
    public int? CameraId { get; set; }
    [JsonPropertyName("timestamp")]
    public DateTime TimeStamp { get; set; }
    [JsonPropertyName("image_width")]
    public int LarguraImagem { get; set; }
    [JsonPropertyName("image_height")]
    public int AlturaImagem { get; set; }
}
