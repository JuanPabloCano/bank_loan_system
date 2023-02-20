using credinet.exception.middleware.models;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuario;
using Domain.Model.Tests.Builders;
using Domain.UseCase.Usuarios;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Tests.Factories.Usuarios;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests.Usuarios;

public class UsuariosUseCaseTest
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly UsuarioUseCase _usuarioUseCase;

    public UsuariosUseCaseTest()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _mockUsuarioRepository = _mockRepository.Create<IUsuarioRepository>();
        _usuarioUseCase = new(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task Obtener_Usuario_Exitoso()
    {
        _mockUsuarioRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(UsuarioFactoryTest.ListarUsuariosTest);

        List<Usuario> usuarios = await _usuarioUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.NotNull(usuarios);
        Assert.NotEmpty(usuarios);
    }

    [Theory]
    [InlineData("26252733")]
    public async Task Obtener_Usuario_PorId_Exitoso(string id)
    {
        var nuevoUsuario = new UsuarioBuilderTest()
            .SetId(id)
            .SetNombre("Juan")
            .Build();

        _mockUsuarioRepository
            .Setup(repository => repository.ObtenerEntidadPorIdAsync(id))
            .ReturnsAsync(nuevoUsuario);

        var usuarioSeleccionado = await _usuarioUseCase.ObtenerEntidadPorId(nuevoUsuario.Id);

        _mockRepository.VerifyAll();
        Assert.NotNull(usuarioSeleccionado);
        Assert.NotEmpty(usuarioSeleccionado.Id);
        Assert.Equal(id, usuarioSeleccionado.Id);
    }

    [Fact]
    public async Task Obtener_Usuarios_SinDatos()
    {
        _mockUsuarioRepository
            .Setup(repository => repository.ListarTodoAsync())
            .ReturnsAsync(new List<Usuario>());

        List<Usuario> usuarios = await _usuarioUseCase.ListarTodo();

        _mockRepository.VerifyAll();
        Assert.Empty(usuarios);
        Assert.NotNull(usuarios);
    }

    [Theory]
    [InlineData("15262", "Juan", "Cano", "62531938", "juan@gmail.com", 26, "Desarrollador")]
    [InlineData("73484", "Ines", "Sald", "42562383", "ines@gmail.com", 52, "Ama de casa")]
    public async Task Crear_Usuario_Exitoso(string id, string nombre, string apellido,
        string cedula, string correo, int edad, string profesion)
    {
        var nuevoUsuario = new UsuarioBuilderTest()
            .SetId(id)
            .SetNombre(nombre)
            .SetApellido(apellido)
            .SetCedula(cedula)
            .SetCorreo(correo)
            .SetEdad(edad)
            .SetProfesion(profesion)
            .Build();

        _mockUsuarioRepository
            .Setup(repository => repository.CrearAsync(It.IsAny<Usuario>()))
            .ReturnsAsync(UsuarioFactoryTest.ObtenerUsuarioTest(id, nombre, apellido,
                cedula, correo, edad, profesion));

        var usuarioCreado = await _usuarioUseCase.Crear(nuevoUsuario);

        _mockRepository.VerifyAll();
        Assert.NotNull(usuarioCreado);
        Assert.NotNull(usuarioCreado.Nombre);
        Assert.Equal(id, usuarioCreado.Id);
    }

    [Theory]
    [InlineData("15262", "Juan", 16)]
    [InlineData("73484", "Ines", -5)]
    public async Task Crear_Usuario_ConEdadMenor_18_Retorna_Excepcion(string id, string nombre, int edad)
    {
        var nuevoUsuario = new UsuarioBuilderTest()
            .SetId(id)
            .SetNombre(nombre)
            .SetEdad(edad)
            .Build();

        BusinessException businessException =
            await Assert.ThrowsAsync<BusinessException>(async () => await _usuarioUseCase.Crear(nuevoUsuario));

        _mockRepository.VerifyAll();
        Assert.Equal((int)TipoExcepcionNegocio.ErrorEdadEntidadIlegal, businessException.code);
    }

    [Theory]
    [InlineData("15262", "Juan", "Cano", "62531938", "juan@gmail.com", 26, "Desarrollador")]
    [InlineData("73484", "Ines", "Sald", "42562383", "ines@gmail.com", 52, "Ama de casa")]
    public async Task Actualizar_Usuario_Exitoso(string id, string nombre, string apellido,
        string cedula, string correo, int edad, string profesion)
    {
        var usuario = UsuarioFactoryTest.ObtenerUsuarioTest(id, nombre, apellido, cedula, correo, edad, profesion);

        var usuarioActualizar = new UsuarioBuilderTest()
            .SetId(usuario.Id)
            .SetNombre(usuario.Nombre)
            .SetApellido("Villegas")
            .Build();

        _mockUsuarioRepository
            .Setup(repository => repository.ActualizarPorIdAsync(usuario.Id, It.IsAny<Usuario>()))
            .ReturnsAsync(usuarioActualizar);

        var usuarioActualizado = await _usuarioUseCase.ActualizarPorId(usuario.Id, usuarioActualizar);

        _mockRepository.VerifyAll();
        Assert.NotNull(usuarioActualizado);
        Assert.NotNull(usuarioActualizado.Nombre);
        Assert.Equal(usuarioActualizado.Id, usuarioActualizado.Id);
        Assert.IsType<Usuario>(usuarioActualizado);
    }
}