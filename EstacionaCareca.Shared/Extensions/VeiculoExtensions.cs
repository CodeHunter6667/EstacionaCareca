using EstacionaCareca.Shared.DTOs;
using EstacionaCareca.Shared.Models;

namespace EstacionaCareca.Shared.Extensions;

public static class VeiculoExtensions
{
    public static Veiculo InsetEntity(this VeiculoDto dto)
    {
        return new Veiculo
        {
            Placa = dto.Placa,
            DataEntrada = DateTime.Now,
        };
    }

    public static VeiculoDto MapToDto(this Veiculo veiculo)
    {
        return new VeiculoDto
        {
            Id = veiculo.Id,
            Placa = veiculo.Placa,
            DataEntrada = veiculo.DataEntrada,
            DataSaida = veiculo.DataSaida,
            ValorPago = veiculo.ValorPago,
            VeiculoSaiu = veiculo.VeiculoSaiu
        };
    }
}
