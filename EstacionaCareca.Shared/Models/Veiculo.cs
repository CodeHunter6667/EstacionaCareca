namespace EstacionaCareca.Shared.Models;

public class Veiculo
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal ValorPago { get; set; }
}
