using System.Data;

namespace ABS.Back.Application.Abstractions.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
