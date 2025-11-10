using EstacionaCareca.Shared.Models;
using Microsoft.Data.SqlClient;

namespace EstacionaCareca.Data.Repositories;

public class VeiculoRepository : Repository<Veiculo>
{
    public VeiculoRepository(SqlConnection connection) : base(connection)
    {
    }
}
