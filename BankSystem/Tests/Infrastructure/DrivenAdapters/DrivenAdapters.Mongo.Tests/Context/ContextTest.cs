using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests.Context;

public class ContextTest
{
    private const string ConnectionString = "mongodb://mongo_db_connection_test";
    private const string DatabaseName = "TestDatabase";
    private readonly Mongo.Context _context;

    public ContextTest()
    {
        Mock<IMongoDatabase> mockDb = new();
        Mock<IMongoClient> mockClient = new();

        mockClient.Setup(client => client.GetDatabase(
                It.IsAny<string>(),
                It.IsAny<MongoDatabaseSettings>()))
            .Returns(mockDb.Object);
        _context = new Mongo.Context(ConnectionString, DatabaseName);
    }

    [Fact]
    public void Obtener_Coleccion_Usuarios_Exitosamente()
    {
        var coleccionUsuarios = _context.Usuarios;
        Assert.NotNull(coleccionUsuarios);
    }

    [Fact]
    public void Obtener_Coleccion_Creditos_Exitosamente()
    {
        var coleccionCreditos = _context.Creditos;
        Assert.NotNull(coleccionCreditos);
    }

    [Fact]
    public void Obtener_Coleccion_Cuentas_Exitosamente()
    {
        var coleccionCuentas = _context.Cuentas;
        Assert.NotNull(coleccionCuentas);
    }

    [Fact]
    public void Obtener_Coleccion_Pagos_Exitosamente()
    {
        var coleccionPagos = _context.Pagos;
        Assert.NotNull(coleccionPagos);
    }
}