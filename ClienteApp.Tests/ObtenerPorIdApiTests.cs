using System.Net;
using System.Net.Http.Json;

namespace ClienteApp.Tests;

public class ObtenerPorIdApiTests
{
    [Fact]
    public async Task ObtenerPorId_ClienteExistente_DebeRetornar200()
    {
        // Arrange: crear un cliente para poder consultarlo
        using var factory = new ClienteApp.Tests.Api.ClienteApiFactory();
        using var client = factory.CreateClient();

        var solicitud = new
        {
            Nombre = "Ana López",
            Correo = $"ana_{Guid.NewGuid():N}@example.com"
        };

        var respuestaCrear = await client.PostAsJsonAsync(
            "/api/Clientes",
            solicitud);

        Assert.Equal(HttpStatusCode.Created, respuestaCrear.StatusCode);

        var creado = await respuestaCrear.Content
            .ReadFromJsonAsync<ClienteRespuesta>();

        Assert.NotNull(creado);

        // Act
        var response = await client.GetAsync(
            $"/api/Clientes/{creado.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerPorId_ClienteInexistente_DebeRetornar404()
    {
        // Arrange
        using var factory = new ClienteApp.Tests.Api.ClienteApiFactory();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Clientes/9999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private sealed record ClienteRespuesta(
        int Id,
        string Nombre,
        string Correo);
}