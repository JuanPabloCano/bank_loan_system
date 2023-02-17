using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests.Builders;
using Domain.Model.ValueObjects;
using Domain.UseCase.Cuentas;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Tests.Factories.Cuentas;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests.Cuentas;

public class CuentaUseCaseTest
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<ICuentaRepository> _mockCuentaRepository;
    private readonly CuentaUseCase _cuentaUseCase;

    public CuentaUseCaseTest()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _mockCuentaRepository = _mockRepository.Create<ICuentaRepository>();
        _cuentaUseCase = new(_mockCuentaRepository.Object);
    }

    [Fact]
    public async Task Obtener_Cuentas_Exitoso()
    {
        _mockCuentaRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(ListarCuentasTest);

        var cuentas = await _cuentaUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(cuentas);
        Assert.NotEmpty(cuentas);
    }

    [Theory]
    [InlineData("26252733")]
    [InlineData("43667665")]
    [InlineData("12123434")]
    public async Task Obtener_Cuenta_PorId_Exitoso(string id)
    {
        var nuevaCuenta = new CuentaBuilderTest()
            .SetId(id)
            .Build();

        _mockCuentaRepository
            .Setup(repository => repository.ObtenerEntidadPorIdAsync(id))
            .ReturnsAsync(nuevaCuenta);

        var cuentaSeleccionada = await _cuentaUseCase.ObtenerEntidadPorId(nuevaCuenta.Id);

        _mockRepository.VerifyAll();
        Assert.NotNull(cuentaSeleccionada);
        Assert.NotEmpty(cuentaSeleccionada.Id);
        Assert.Equal(id, cuentaSeleccionada.Id);
    }

    [Fact]
    public async Task Obtener_Cuentas_SinDatos()
    {
        _mockCuentaRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(new List<Cuenta>());

        var cuentas = await _cuentaUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(cuentas);
        Assert.Empty(cuentas);
    }

    [Fact]
    public async Task Crear_Cuenta_Exitoso()
    {
        var nuevaCuenta = CuentaFactoryTest.ObtenerCuentaTest(
            "52323",
            "627237",
            new Banco(
                "Bancolombia",
                "Sabaneta",
                "Cra 43A #75"),
            20000);

        _mockCuentaRepository
            .Setup(repository => repository.CrearAsync(It.IsAny<Cuenta>()))
            .ReturnsAsync(nuevaCuenta);

        var cuentaCreada = await _cuentaUseCase.Crear(nuevaCuenta);

        _mockRepository.VerifyAll();
        Assert.NotNull(cuentaCreada);
        Assert.NotNull(cuentaCreada.UsuarioId);
        Assert.Equal(nuevaCuenta.Id, cuentaCreada.Id);
        Assert.NotNull(cuentaCreada.Banco.Ciudad);
        Assert.IsType<Cuenta>(cuentaCreada);
    }

    [Theory]
    [InlineData("6256237", "237238", 0)]
    [InlineData("8347236", "763623", -43)]
    public async Task Crear_Cuenta_ConCapacidadEndeudamiento_Menor_0_Retorna_Excepcion(string id, string usuarioId,
        int capacidadEndeudamiento)
    {
        var nuevaCuenta = new CuentaBuilderTest()
            .SetId(id)
            .SetUsuarioId(usuarioId)
            .SetCapacidadEndeudamiento(capacidadEndeudamiento)
            .Build();

        BusinessException businessException =
            await Assert.ThrowsAsync<BusinessException>(async () => await _cuentaUseCase.Crear(nuevaCuenta));

        _mockRepository.VerifyAll();
        Assert.Equal((int)TipoExcepcionNegocio.ErrorCapacidadEndeudamientoMenorOIgualCero, businessException.code);
    }

    [Theory]
    [InlineData("6256237", "237238", 50000)]
    [InlineData("8347236", "763623", 35000)]
    public async Task Actualizar_Cuenta_Exitoso(string id, string usuarioId,
        int capacidadEndeudamiento)
    {
        var cuenta = new CuentaBuilderTest()
            .SetId(id)
            .SetUsuarioId(usuarioId)
            .SetCapacidadEndeudamiento(capacidadEndeudamiento)
            .Build();

        var cuentaActualizar = new CuentaBuilderTest()
            .SetId(cuenta.Id)
            .SetUsuarioId(cuenta.UsuarioId)
            .SetCapacidadEndeudamiento(15000)
            .Build();

        _mockCuentaRepository
            .Setup(repository => repository.ActualizarPorIdAsync(cuenta.Id, It.IsAny<Cuenta>()))
            .ReturnsAsync(cuentaActualizar);

        var cuentaActualizada = await _cuentaUseCase.ActualizarPorId(id, cuentaActualizar);

        _mockRepository.VerifyAll();
        Assert.NotNull(cuentaActualizada);
        Assert.NotNull(cuentaActualizada.UsuarioId);
        Assert.Equal(cuentaActualizar.Id, cuentaActualizada.Id);
        Assert.IsType<Cuenta>(cuentaActualizada);
    }

    #region Private Methods

    private static List<Cuenta> ListarCuentasTest() => new()
    {
        new CuentaBuilderTest()
            .SetId("435345")
            .SetUsuarioId("3478348")
            .SetBanco(new Banco(
                "BBVA",
                "Sabaneta",
                "Cra 43A # 75"))
            .SetCapacidadEndeudamiento(20000)
            .Build(),
        new CuentaBuilderTest()
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