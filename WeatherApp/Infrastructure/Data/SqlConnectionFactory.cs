using Application.Common.Interfaces.Data;
using Npgsql;
using System.Data;

namespace Infrastructure.Data;
public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
{
    private IDbConnection _connection;
    private string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<IDbConnection> GetOpenConnection()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        return Task.FromResult(_connection);
    }

    public void Dispose()
    {
        if (_connection != null && _connection.State == ConnectionState.Open)
        {
            _connection.Dispose();
        }
    }
}