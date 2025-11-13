using EstacionaCareca.Servico.DTO;
using EstacionaCareca.Shared.Models;

namespace EstacionaCareca.Servico.Extensions;

public static class VeiculoExtension
{
    public static Veiculo MapToVeiculo(this VeiculoDTO dto)
    {
        return new Veiculo
        {
            Placa = dto.Placa,
            DataEntrada = dto.DataEntrada,
            DataSaida = dto.DataSaida,
            ValorPago = dto.ValorPago
        };
    }
}
