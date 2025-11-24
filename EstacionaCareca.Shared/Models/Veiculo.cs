namespace EstacionaCareca.Shared.Models;

public class Veiculo
{
    private decimal _valorMinimoEstacionamento = 5.00m;
    private decimal _acrescimoPorHora = 3.00m;

    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal ValorPago { get; set; }
    public bool VeiculoSaiu { get; set; }

    public void MarcarSaida()
    {
        DataSaida = DateTime.Now;
        ValorPago = CalcularValorEstacionamento();
        VeiculoSaiu = true;
    }

    private decimal CalcularValorEstacionamento()
    {
        var valorAPagar = 0.00m;
        var tempoAtual = DateTime.Now;
        var duracao = tempoAtual.Minute - DataEntrada.Minute;

        if (duracao <= 15)
            return valorAPagar;
        else if (duracao > 15 && duracao <= 60)
            return _valorMinimoEstacionamento;
        else
        {
            var duracaoAcrescimo = duracao - 60;
            var horasExtras = Math.Ceiling(duracaoAcrescimo / 60.0);
            valorAPagar = _valorMinimoEstacionamento + (decimal)horasExtras * _acrescimoPorHora;
            return valorAPagar;
        }
    }

}
