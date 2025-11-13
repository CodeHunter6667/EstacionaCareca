namespace EstacionaCareca.Servico.Services;

using EstacionaCareca.Data.Repositories;
using EstacionaCareca.Servico.DTO;
using EstacionaCareca.Servico.Extensions;
using EstacionaCareca.Shared.Models;

public class VeiculoService(VeiculoRepository repository)
{

    public void EntradaVeiculo(VeiculoDTO dto)
    {
        var veiculo = dto.MapToVeiculo();
        veiculo.DataEntrada = DateTime.Now;
        repository.Create(veiculo);
    }

    public void SaidaVeiculo(string placa)
    {
        var veiculo = BuscaPlaca(placa);
        veiculo.DataSaida = DateTime.Now;
        veiculo.ValorPago = CalculaValor(veiculo.DataEntrada, veiculo.DataSaida.Value);
        repository.Update(veiculo);
    }

    private Veiculo BuscaPlaca(string placa)
    {
        var veiculo = repository.GetByPlaca(placa) ?? throw new NullReferenceException("Veículo não encontrado.");
        return veiculo;
    }

    private decimal CalculaHoras(DateTime entrada, DateTime saida)
    {
        var horas = (decimal)(saida - entrada).TotalHours;
        return Math.Ceiling(horas);
    }

    private decimal CalculaValor(DateTime entrada, DateTime saida)
    {
        var horas = CalculaHoras(entrada, saida);
        if (horas < 0.15m)
        {
            return 0;
        }
        else if (horas <= 1)
        {
            return 5;
        }
        else
        {
            return 5 + (horas - 1) * 3;
        }
    }
}
