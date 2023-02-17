using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Adapters;
using DrivenAdapters.Mongo.Entities.Cuentas;
using DrivenAdapters.Mongo.Tests.Builders;
using Helpers.ObjectsUtils.Tests.Factories.Cuentas;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests.Adapters;

public class CuentaRepositoryAdapterTest
{
    private readonly Mock<IContext> _mockContext;
    private readonly Mock<IContext> _mockContextUsuario;
    private readonly Mock<IMongoCollection<CuentaData>> _mockColeccionCuenta;
    private readonly Mock<IAsyncCursor<CuentaData>> _mockCuentaCursor;
    private readonly IUsuarioRepository _usuarioRepository;

    public CuentaRepositoryAdapterTest()
    {
        _mockContext = new Mock<IContext>();
        _mockContextUsuario = new Mock<IContext>();
        _mockColeccionCuenta = new Mock<IMongoCollection<CuentaData>>();
        _mockCuentaCursor = new Mock<IAsyncCursor<CuentaData>>();

        _mockColeccionCuenta.Object.InsertMany(ListarCuentasTest());

        _mockCuentaCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true).Returns(false);

        _mockCuentaCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

        _usuarioRepository = new UsuarioRepositoryAdapter(_mockContextUsuario.Object);
    }

    [Fact]
    public async Task Obtener_Coleccion_Cuentas_Exitoso()
    {
        _mockCuentaCursor.Setup(cuenta => cuenta.Current).Returns(ListarCuentasTest);

        _mockColeccionCuenta.Setup(cuentaData =>
                cuentaData.FindAsync(
                    It.IsAny<FilterDefinition<CuentaData>>(),
                    It.IsAny<FindOptions<CuentaData, CuentaData>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockCuentaCursor.Object);

        _mockContext.Setup(context => context.Cuentas).Returns(_mockColeccionCuenta.Object);

        var cuentaRepository = new CuentaRepositoryAdapter(_mockContext.Object, _usuarioRepository);

        var listaCuentas = await cuentaRepository.ListarTodoAsync();

        Assert.NotNull(listaCuentas);
        Assert.NotEmpty(listaCuentas);
    }

    [Fact]
    public async Task Obtener_Coleccion_Vacia_Cuentas_Retorna_Lista_Vacia()
    {
        _mockColeccionCuenta.Setup(cuentaData =>
                cuentaData.FindAsync(
                    It.IsAny<FilterDefinition<CuentaData>>(),
                    It.IsAny<FindOptions<CuentaData, CuentaData>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockCuentaCursor.Object);

        _mockContext.Setup(context => context.Cuentas).Returns(_mockColeccionCuenta.Object);

        var cuentaRepository = new CuentaRepositoryAdapter(_mockContext.Object, _usuarioRepository);

        var listaUsuarios = await cuentaRepository.ListarTodoAsync();

        Assert.Empty(listaUsuarios);
    }

    [Fact]
    public async Task Crear_Cuenta_Exitoso()
    {
        var cuenta = CuentaFactoryTest.ObtenerCuentaTest(
            "252522",
            "623775",
            new Banco(
                "BBVA",
                "Sabaneta",
                "Cra 43A #75"),
            20000);

        _mockColeccionCuenta.Setup(cuentaData =>
            cuentaData.InsertOneAsync(
                It.IsAny<CuentaData>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()));

        _mockContext.Setup(context => context.Cuentas).Returns(_mockColeccionCuenta.Object);

        var cuentaRepository = new CuentaRepositoryAdapter(_mockContext.Object, _usuarioRepository);

        var cuentaCreada = await cuentaRepository.CrearAsync(cuenta);

        Assert.NotNull(cuentaCreada);
        Assert.Equal(cuenta.UsuarioId, cuentaCreada.UsuarioId);
        Assert.IsType<Cuenta>(cuentaCreada);
    }

    #region Private Methods

    private static List<CuentaData> ListarCuentasTest() => new()
    {
        new CuentaDataBuilderTest()
            .SetId("435345")
            .SetUsuarioId("3478348")
            .SetBanco(new Banco(
                "BBVA",
                "Sabaneta",
                "Cra 43A # 75"))
            .SetCapacidadEndeudamiento(20000)
            .Build(),
        new CuentaDataBuilderTest()
            .SetId("9090789")
            .SetUsuarioId("76653223")
            .SetBanco(new Banco(
                "Bancolombia",
                "Envigado",
                "Cl 23 # 29"))
            .SetCapacidadEndeudamiento(30000)
            .Build()
    };

    #endregion Private Methods
}