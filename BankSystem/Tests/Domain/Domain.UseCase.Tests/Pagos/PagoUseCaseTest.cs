using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Pago;
using Domain.Model.Tests.Builders;
using Domain.UseCase.Pagos;
using Helpers.ObjectsUtils.Tests.Factories.Pagos;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests.Pagos;

public class PagoUseCaseTest
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<IPagoRepository> _mockPagoRepository;
    private readonly Mock<ICreditoRepository> _mockCreditoRepository;
    private readonly PagoUseCase _pagoUseCase;

    public PagoUseCaseTest()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _mockPagoRepository = new Mock<IPagoRepository>();
        _mockCreditoRepository = new Mock<ICreditoRepository>();
        _pagoUseCase = new PagoUseCase(_mockPagoRepository.Object, _mockCreditoRepository.Object);
    }

    [Fact]
    public async Task Obtener_Pago_Exitoso()
    {
        _mockPagoRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(PagoFactoryTest.ListarPagosTest);

        var pagos = await _pagoUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(pagos);
        Assert.NotEmpty(pagos);
    }

    [Theory]
    [InlineData("26252733")]
    [InlineData("43667665")]
    [InlineData("12123434")]
    public async Task Obtener_Pago_PorId_Exitoso(string id)
    {
        var nuevoPago = new PagoBuilderTest()
            .SetId(id)
            .Build();

        _mockPagoRepository
            .Setup(repository => repository.ObtenerEntidadPorIdAsync(id))
            .ReturnsAsync(nuevoPago);

        var pagoSeleccionado = await _pagoUseCase.ObtenerEntidadPorId(nuevoPago.Id);

        _mockRepository.VerifyAll();
        Assert.NotNull(pagoSeleccionado);
        Assert.NotEmpty(pagoSeleccionado.Id);
        Assert.Equal(id, pagoSeleccionado.Id);
    }

    [Fact]
    public async Task Obtener_Pago_SinDatos()
    {
        _mockPagoRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(new List<Pago>());

        var pagos = await _pagoUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(pagos);
        Assert.Empty(pagos);
    }
}