using Domain.Model.Entities.Credito;
using DrivenAdapters.Mongo.Adapters;
using DrivenAdapters.Mongo.Entities.Creditos;
using DrivenAdapters.Mongo.Tests.Builders;
using Helpers.ObjectsUtils.Tests.Factories.Creditos;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests.Adapters;

public class CreditoRepositoryAdapterTest
{
    private readonly Mock<IContext> _mockContext;
    private readonly Mock<IMongoCollection<CreditoData>> _mockColeccionCredito;
    private readonly Mock<IAsyncCursor<CreditoData>> _mockCreditoCursor;

    public CreditoRepositoryAdapterTest()
    {
        _mockContext = new Mock<IContext>();
        _mockColeccionCredito = new Mock<IMongoCollection<CreditoData>>();
        _mockCreditoCursor = new Mock<IAsyncCursor<CreditoData>>();

        _mockColeccionCredito.Object.InsertMany(ListarCreditosTest());

        _mockCreditoCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true).Returns(false);

        _mockCreditoCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
    }

    [Fact]
    public async Task Obtener_Coleccion_Creditos_Exitoso()
    {
        _mockCreditoCursor.Setup(credito => credito.Current).Returns(ListarCreditosTest);

        _mockColeccionCredito
            .Setup(creditoData =>
                creditoData.FindAsync(
                    It.IsAny<FilterDefinition<CreditoData>>(),
                    It.IsAny<FindOptions<CreditoData, CreditoData>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockCreditoCursor.Object);

        _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionCredito.Object);

        var creditoRepository = new CreditoRepositoryAdapter(_mockContext.Object);

        var listaCreditos = await creditoRepository.ListarTodoAsync();

        Assert.NotNull(listaCreditos);
        Assert.NotEmpty(listaCreditos);
    }

    [Fact]
    public async Task Obtener_Coleccion_Vacia_Creditos_Retorna_Lista_Vacia()
    {
        _mockColeccionCredito
            .Setup(creditoData =>
                creditoData.FindAsync(
                    It.IsAny<FilterDefinition<CreditoData>>(),
                    It.IsAny<FindOptions<CreditoData, CreditoData>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockCreditoCursor.Object);

        _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionCredito.Object);

        var creditoRepository = new CreditoRepositoryAdapter(_mockContext.Object);

        var listaCreditos = await creditoRepository.ListarTodoAsync();

        Assert.Empty(listaCreditos);
    }

    [Fact]
    public async Task Crear_Credito_Exitoso()
    {
        var credito = CreditoFactoryTest.ObtenerCredito(
            "252522",
            "623775",
            "747848",
            20000,
            20000);

        _mockColeccionCredito
            .Setup(creditoData =>
                creditoData.InsertOneAsync(
                    It.IsAny<CreditoData>(),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()));

        _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionCredito.Object);

        var creditoRepository = new CreditoRepositoryAdapter(_mockContext.Object);

        var creditoCreado = await creditoRepository.CrearAsync(credito);

        Assert.NotNull(creditoCreado);
        Assert.Equal(credito.Id, credito.Id);
        Assert.IsType<Credito>(creditoCreado);
    }

    #region Private Methods

    private List<CreditoData> ListarCreditosTest() => new()
    {
        new CreditoDataBuilderTest()
            .SetId("272367")
            .SetUsuarioId("2367326")
            .SetCuentaId("2367296")
            .SetTotalPrestamo(34000)
            .SetDeudaActual(34000)
            .Build(),
        new CreditoDataBuilderTest()
            .SetId("667898")
            .SetUsuarioId("07886795")
            .SetCuentaId("76846546")
            .SetTotalPrestamo(12000)
            .SetDeudaActual(12000)
            .Build()
    };

    #endregion Private Methods
}