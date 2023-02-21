using System.Net;
using credinet.exception.middleware.models;
using Domain.Model.Entities.Usuario;
using Domain.Model.Tests.Builders;
using Domain.UseCase.Common;
using Domain.UseCase.Usuarios;
using EntryPoints.ReactiveWeb.Controllers.Usuarios;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Tests.Factories.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace EntryPoints.ReactWeb.Tests.Controllers.Usuarios;

public class UsuarioControllerTest
{
    private readonly Mock<IUsuarioUseCase> _mockUsuarioUseCase;
    private readonly UsuarioController _usuarioController;

    public UsuarioControllerTest()
    {
        Mock<IOptions<ConfiguradorAppSettings>> mockAppSettings = new();
        Mock<IManageEventsUseCase> mockManageEventsUseCase = new();
        _mockUsuarioUseCase = new Mock<IUsuarioUseCase>();

        mockAppSettings.Setup(options => options.Value)
            .Returns(new ConfiguradorAppSettings()
            {
                DefaultCountry = "co",
                DomainName = "Usuarios"
            });

        _usuarioController = new UsuarioController(mockManageEventsUseCase.Object, _mockUsuarioUseCase.Object);

        _usuarioController.ControllerContext.HttpContext = new DefaultHttpContext();
        _usuarioController.ControllerContext.HttpContext.Request.Headers["Location"] = "1,1";
        _usuarioController.ControllerContext.RouteData = new RouteData();
        _usuarioController.ControllerContext.RouteData.Values.Add("controller", "Usuarios");
    }

    [Theory]
    [InlineData("15262", "Juan", "Cano", "62531938", "juan@gmail.com", 26, "Desarrollador")]
    [InlineData("73484", "Ines", "Sald", "42562383", "ines@gmail.com", 52, "Ama de casa")]
    public async Task Crear_Usuario_ConStatus_200(string id, string nombre, string apellido,
        string cedula, string correo, int edad, string profesion)
    {
        var usuarioRequest =
            UsuarioFactoryTest.ObtenerUsuarioRequestTest(id, nombre, apellido, cedula, correo, edad, profesion);

        _mockUsuarioUseCase
            .Setup(useCase => useCase.Crear(It.IsAny<Usuario>()))
            .ReturnsAsync(UsuarioFactoryTest.ObtenerUsuarioTest(id, nombre, apellido, cedula, correo, edad, profesion));

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "CrearUsuario");

        var result = await _usuarioController.CrearUsuario(usuarioRequest);
        var okObjectResult = result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ObtenerUsuarios_Con_Status200()
    {
        _mockUsuarioUseCase
            .Setup(useCase => useCase.ListarTodo())
            .ReturnsAsync(UsuarioFactoryTest.ListarUsuariosTest);

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "ObtenerUsuarios");

        var result = await _usuarioController.ListarUsuarios();

        var okObjectResult = result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("5563")]
    [InlineData("8695")]
    public async Task ObtenerUsuario_PorId_Con_Status200(string id)
    {
        var usuario = new UsuarioBuilderTest()
            .SetId(id)
            .Build();

        _mockUsuarioUseCase
            .Setup(useCase => useCase.ObtenerEntidadPorId(id))
            .ReturnsAsync(usuario);

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "ObtenerUsuarioPorId");

        var result = await _usuarioController.ObtenerUsuarioPorId(id);

        var okObjectResult = result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(id, usuario.Id);
    }

    [Fact]
    public async Task CrearUsuario_Con_Excepcion_De_Negocio()
    {
        var usuarioRequest = UsuarioFactoryTest.ObtenerUsuarioRequestTest(string.Empty, string.Empty,
            string.Empty, string.Empty, string.Empty, 0, string.Empty);

        _mockUsuarioUseCase
            .Setup(useCase => useCase.Crear(It.IsAny<Usuario>()))
            .Throws(new BusinessException(nameof(TipoExcepcionNegocio.ErrorEdadEntidadIlegal),
                (int)TipoExcepcionNegocio.ErrorEdadEntidadIlegal));

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "CrearUsuario");

        var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
            await _usuarioController.CrearUsuario(usuarioRequest));

        Assert.Equal((int)TipoExcepcionNegocio.ErrorEdadEntidadIlegal, exception.code);
    }
}