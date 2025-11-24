namespace EstacionaCareca.Shared.DTOs;

public class VeiculoDto
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal ValorPago { get; set; }
    public bool VeiculoSaiu { get; set; }
}
