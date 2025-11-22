using System.Text.Json.Serialization;

namespace EstacionaCareca.Shared.DTOs;

public class ResultadoDto
{
    [JsonPropertyName("plate")]
    public string Placa { get; set; } = string.Empty;
    [JsonPropertyName("region")]
    public Regiao Regiao { get; set; } = new Regiao();
}

public class Regiao
{
    [JsonPropertyName("code")]
    public string Codigo { get; set; } = string.Empty;
    [JsonPropertyName("score")]
    public decimal Classificacao { get; set; }
}