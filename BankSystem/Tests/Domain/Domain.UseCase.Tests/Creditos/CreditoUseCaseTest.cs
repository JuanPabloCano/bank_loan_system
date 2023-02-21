using credinet.exception.middleware.models;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests.Builders;
using Domain.UseCase.Creditos;
using Helpers.Commons.Exceptions;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests.Creditos;

public class CreditoUseCaseTest
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<ICreditoRepository> _mockCreditoRepository;
    private readonly Mock<ICuentaRepository> _mockCuentaRepository;
    private readonly CreditoUseCase _creditoUseCase;

    public CreditoUseCaseTest()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _mockCreditoRepository = new Mock<ICreditoRepository>();
        _mockCuentaRepository = new Mock<ICuentaRepository>();
        _creditoUseCase = new(_mockCreditoRepository.Object, _mockCuentaRepository.Object);
    }

    [Fact]
    public async Task Obtener_Creditos_Exitoso()
    {
        _mockCreditoRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(ListarCreditosTest);

        var creditos = await _creditoUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(creditos);
        Assert.NotEmpty(creditos);
    }

    [Theory]
    [InlineData("26252733")]
    [InlineData("43667665")]
    [InlineData("12123434")]
    public async Task Obtener_Credito_PorId_Exitoso(string id)
    {
        var nuevoCredito = new CreditoBuilderTest()
            .SetId(id)
            .Build();

        _mockCreditoRepository
            .Setup(repository => repository.ObtenerEntidadPorIdAsync(id))
            .ReturnsAsync(nuevoCredito);

        var creditoSeleccionado = await _creditoUseCase.ObtenerEntidadPorId(nuevoCredito.Id);

        _mockRepository.VerifyAll();
        Assert.NotNull(creditoSeleccionado);
        Assert.NotEmpty(creditoSeleccionado.Id);
        Assert.Equal(id, creditoSeleccionado.Id);
    }

    [Fact]
    public async Task Obtener_Creditos_SinDatos()
    {
        _mockCreditoRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(new List<Credito>());

        var creditos = await _creditoUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(creditos);
        Assert.Empty(creditos);
    }

    [Theory]
    [InlineData("23123", "234324", "8745783", 20000, 20000)]
    [InlineData("73623", "123445", "3415372", 10000, 10000)]
    public async Task Crear_Credito_Exitoso(string id, string usuarioId, string cuentaId, int totalPrestamo,
        int deudaActual)
    {
        var nuevoCredito = new CreditoBuilderTest()
            .SetId(id)
            .SetUsuarioId(usuarioId)
            .SetCuentaId(cuentaId)
            .SetTotalPrestamo(totalPrestamo)
            .SetDeudaActual(deudaActual)
            .Build();

        var cuenta = new CuentaBuilderTest()
            .SetId(cuentaId)
            .SetCapacidadEndeudamiento(50000)
            .Build();

        _mockCreditoRepository
            .Setup(repository => repository.CrearAsync(It.IsAny<Credito>()))
            .ReturnsAsync(nuevoCredito);

        _mockCuentaRepository
            .Setup(repository => repository.ObtenerEntidadPorIdAsync(cuentaId))
            .ReturnsAsync(cuenta);

        var creditoCreado = await _creditoUseCase.Crear(nuevoCredito);

        _mockRepository.VerifyAll();
        Assert.NotNull(creditoCreado);
        Assert.NotNull(creditoCreado.UsuarioId);
        Assert.Equal(nuevoCredito.Id, creditoCreado.Id);
        Assert.Equal(cuenta.Id, creditoCreado.CuentaId);
        Assert.IsType<Credito>(creditoCreado);
        Assert.IsType<Cuenta>(cuenta);
    }

    [Theory]
    [InlineData("23123", "234324", "8745783", 20000, 20000)]
    [InlineData("73623", "123445", "3415372", 10000, 10000)]
    public async Task Crear_Credito_ConCapacidadEndeudamientoMenor_Retorna_Excepcion(string id, string usuarioId,
        string cuentaId, int totalPrestamo,
        int deudaActual)
    {
        var nuevoCredito = new CreditoBuilderTest()
            .SetId(id)
            .SetUsuarioId(usuarioId)
            .SetCuentaId(cuentaId)
            .SetTotalPrestamo(totalPrestamo)
            .SetDeudaActual(deudaActual)
            .Build();

        var cuenta = new CuentaBuilderTest()
            .SetId(cuentaId)
            .SetCapacidadEndeudamiento(1000)
            .Build();

        _mockCuentaRepository
            .Setup(repository => repository.ObtenerEntidadPorIdAsync(cuentaId))
            .ReturnsAsync(cuenta);

        BusinessException businessException =
            await Assert.ThrowsAsync<BusinessException>(async () => await _creditoUseCase.Crear(nuevoCredito));

        _mockRepository.VerifyAll();
        Assert.Equal((int)TipoExcepcionNegocio.ErrorCapacidadEndeudamientoInvalida, businessException.code);
    }

    [Theory]
    [InlineData("6256237", "237238", 50000)]
    [InlineData("8347236", "763623", 35000)]
    public async Task Actualizar_Credito_Exitoso(string id, string usuarioId,
        int totalPrestamo)
    {
        var credito = new CreditoBuilderTest()
            .SetId(id)
            .SetUsuarioId(usuarioId)
            .SetTotalPrestamo(totalPrestamo)
            .Build();

        var creditoActualizar = new CreditoBuilderTest()
            .SetId(credito.Id)
            .SetUsuarioId(credito.UsuarioId)
            .SetTotalPrestamo(60000)
            .Build();

        _mockCreditoRepository
            .Setup(repository => repository.ActualizarPorIdAsync(credito.Id, It.IsAny<Credito>()))
            .ReturnsAsync(creditoActualizar);

        var creditoActualizado = await _creditoUseCase.ActualizarPorId(id, creditoActualizar);

        _mockRepository.VerifyAll();
        Assert.NotNull(creditoActualizado);
        Assert.NotNull(creditoActualizado.UsuarioId);
        Assert.Equal(creditoActualizado.Id, creditoActualizado.Id);
        Assert.IsType<Credito>(creditoActualizado);
    }

    #region MyRegion

    private List<Credito> ListarCreditosTest() => new()
    {
        new CreditoBuilderTest()
            .SetId("234324")
            .SetUsuarioId("124143")
            .SetCuentaId("34623723")
            .SetTotalPrestamo(20000)
            .SetDeudaActual(20000)
            .Build(),
        new CreditoBuilderTest()
            .SetId("787686")
            .SetUsuarioId("454325")
            .SetCuentaId("5435342")
            .SetTotalPrestamo(15000)
            .SetDeudaActual(15000)
            .Build()
    };

    #endregion MyRegion
}