using ClienteApp.Core.Models;
using ClienteApp.Core.Services;
using Moq;

namespace ClienteApp.Tests;

public class ObtenerPorIdUnitariaTests
{
    [Fact]
    public void ObtenerPorId_ClienteExistente_DevuelveClienteEsperadoYConsultaRepositorioUnaVez()
    {
        // Arrange
        var clienteEsperado = new Cliente
        {
            Id = 1,
            Nombre = "Ana López",
            Correo = "ana@example.com"
        };

        var repositoryMock = new Mock<IClienteRepository>();
        repositoryMock
            .Setup(r => r.ObtenerPorId(1))
            .Returns(clienteEsperado);

        var service = new ClienteService(repositoryMock.Object);

        // Act
        var resultado = service.ObtenerPorId(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.Id);
        Assert.Equal("Ana López", resultado.Nombre);
        Assert.Equal("ana@example.com", resultado.Correo);

        repositoryMock.Verify(
            r => r.ObtenerPorId(1),
            Times.Once);
    }
}