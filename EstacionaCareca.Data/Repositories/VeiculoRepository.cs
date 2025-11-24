using Dapper;
using EstacionaCareca.Shared.Models;
using Microsoft.Data.SqlClient;

namespace EstacionaCareca.Data.Repositories;

public class VeiculoRepository(SqlConnection connection) : Repository<Veiculo>(connection)
{
    public Veiculo? GetByPlate(string plate)
    {
        var sql = "SELECT * FROM Veiculo WHERE Placa = @Placa AND VeiculoSaiu = 0";
        var veiculo = connection.ExecuteScalar<Veiculo>(sql, new { Placa = plate });
        return veiculo;
    }
}
