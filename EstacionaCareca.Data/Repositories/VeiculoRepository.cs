using Dapper;
using EstacionaCareca.Shared.Models;
using Microsoft.Data.SqlClient;

namespace EstacionaCareca.Data.Repositories;

public class VeiculoRepository : Repository<Veiculo>
{
    private readonly SqlConnection _connection;
    public VeiculoRepository(SqlConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public Veiculo? GetByPlaca(string placa)
    {
        string query = @"SELECT * FROM Veiculo WHERE Placa = @Placa";
        var veiculo = _connection.ExecuteScalar<Veiculo>(query, new { Placa = placa });
        return veiculo;
    }
}
