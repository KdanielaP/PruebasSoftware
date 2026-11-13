using ClienteApp.Core.Data;
using ClienteApp.Core.Repositories;
using ClienteApp.Core.Services;

namespace ClienteApp.Tests;

public class ObtenerPorIdIntegracionTests : IDisposable
{
    private readonly string _dbPath;
    private readonly ClienteService _service;

    public ObtenerPorIdIntegracionTests()
    {
        _dbPath = Path.Combine(
            Path.GetTempPath(),
            $"clientes_obtener_{Guid.NewGuid():N}.db");

        var connectionString = $"Data Source={_dbPath};Pooling=False";

        Database.Inicializar(connectionString);

        var repository = new ClienteRepository(connectionString);
        _service = new ClienteService(repository);
    }

    [Fact]
    public void ObtenerPorId_IdInexistente_DebeRetornarNulo()
    {
        // Act
        var resultado = _service.ObtenerPorId(9999);

        // Assert
        Assert.Null(resultado);
    }

    public void Dispose()
    {
        if (File.Exists(_dbPath))
        {
            File.Delete(_dbPath);
        }
    }
}