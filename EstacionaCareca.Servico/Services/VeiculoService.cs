using EstacionaCareca.Data.Repositories;
using EstacionaCareca.Shared.DTOs;
using EstacionaCareca.Shared.Extensions;
using EstacionaCareca.Shared.Models;

namespace EstacionaCareca.Servico.Services;

public class VeiculoService(VeiculoRepository repository)
{
    public async Task<Veiculo> InserirVeiculo(VeiculoDto dto)
    {
        var veiculo = dto.InsetEntity();
        veiculo = repository.Create(veiculo);
        return veiculo;
    }

    public async Task<Veiculo?> MarcarSaidaVeiculo(VeiculoDto dto)
    {
        var veiculo = repository.GetByPlate(dto.Placa);
        if (veiculo == null)
            return await InserirVeiculo(dto);
        veiculo.MarcarSaida();
        repository.Update(veiculo);
        return veiculo;
    }
}
