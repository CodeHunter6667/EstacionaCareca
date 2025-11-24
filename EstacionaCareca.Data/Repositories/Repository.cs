using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace EstacionaCareca.Data.Repositories;

public class Repository<T>(SqlConnection connection) where T : class
{
    public IEnumerable<T> Get()
        => connection.GetAll<T>();

    public T GetById(int id)
        => connection.Get<T>(id);

    public T Create(T model)
    {
        var id = connection.Insert(model);
        return connection.Get<T>((int)id);
    }

    public void Update(T model)
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null && (int)idProperty.GetValue(model) != 0)
        {
            connection.Update(model);
        }
    }

    public void Delete(int id)
    {
        var model = connection.Get<T>(id);
        connection.Delete(model);
    }

    public void Delete(T model)
        => connection.Delete(model);
}
