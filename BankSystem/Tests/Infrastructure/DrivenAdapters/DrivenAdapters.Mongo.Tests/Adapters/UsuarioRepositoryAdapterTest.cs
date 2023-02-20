using Domain.Model.Entities.Usuario;
using Domain.Model.Tests.Builders;
using DrivenAdapters.Mongo.Adapters;
using DrivenAdapters.Mongo.Entities.Usuarios;
using DrivenAdapters.Mongo.Tests.Builders;
using Helpers.ObjectsUtils.Tests.Factories.Usuarios;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests.Adapters;

public class UsuarioRepositoryAdapterTest
{
    private readonly Mock<IContext> _mockContext;
    private readonly Mock<IMongoCollection<UsuarioData>> _mockColeccionUsuario;
    private readonly Mock<IAsyncCursor<UsuarioData>> _mockUsuarioCursor;

    public UsuarioRepositoryAdapterTest()
    {
        _mockContext = new Mock<IContext>();
        _mockColeccionUsuario = new Mock<IMongoCollection<UsuarioData>>();
        _mockUsuarioCursor = new Mock<IAsyncCursor<UsuarioData>>();

        _mockColeccionUsuario.Object.InsertMany(ListarUsuariosTest());

        _mockUsuarioCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true).Returns(false);

        _mockUsuarioCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
    }

    [Fact]
    public async Task Obtener_Coleccion_Usuarios_Exitoso()
    {
        _mockUsuarioCursor.Setup(usuario => usuario.Current).Returns(ListarUsuariosTest);

        _mockColeccionUsuario.Setup(usuarioData =>
                usuarioData.FindAsync(
                    It.IsAny<FilterDefinition<UsuarioData>>(),
                    It.IsAny<FindOptions<UsuarioData, UsuarioData>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockUsuarioCursor.Object);

        _mockContext.Setup(context => context.Usuarios).Returns(_mockColeccionUsuario.Object);

        var usuarioRepository = new UsuarioRepositoryAdapter(_mockContext.Object);

        var listaUsuarios = await usuarioRepository.ListarTodoAsync();

        Assert.NotNull(listaUsuarios);
        Assert.NotEmpty(listaUsuarios);
    }

    [Fact]
    public async Task Obtener_Coleccion_Vacia_Usuarios_Retorna_Lista_Vacia()
    {
        _mockColeccionUsuario.Setup(usuarioData =>
                usuarioData.FindAsync(
                    It.IsAny<FilterDefinition<UsuarioData>>(),
                    It.IsAny<FindOptions<UsuarioData, UsuarioData>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockUsuarioCursor.Object);

        _mockContext.Setup(context => context.Usuarios).Returns(_mockColeccionUsuario.Object);

        var usuarioRepository = new UsuarioRepositoryAdapter(_mockContext.Object);

        var listaUsuarios = await usuarioRepository.ListarTodoAsync();

        Assert.Empty(listaUsuarios);
    }

    [Theory]
    [InlineData("15262", "Juan", "Cano", "62531938", "juan@gmail.com", 26, "Desarrollador")]
    [InlineData("73484", "Ines", "Sald", "42562383", "ines@gmail.com", 52, "Ama de casa")]
    public async Task Crear_Usuario_Exitoso(string id, string nombre, string apellido,
        string cedula, string correo, int edad, string profesion)
    {
        _mockColeccionUsuario.Setup(usuarioData =>
            usuarioData.InsertOneAsync(
                It.IsAny<UsuarioData>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()));

        _mockContext.Setup(context => context.Usuarios).Returns(_mockColeccionUsuario.Object);

        var usuarioRepository = new UsuarioRepositoryAdapter(_mockContext.Object);

        var usuario = await usuarioRepository.CrearAsync(UsuarioFactoryTest.ObtenerUsuarioTest(id, nombre, apellido,
            cedula, correo, edad, profesion));

        Assert.NotNull(usuario);
        Assert.Equal(profesion, usuario.Profesion);
        Assert.IsType<Usuario>(usuario);
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
            .SetNombre("Carmen")
            .SetApellido("Villagran")
            .Build();

        _mockColeccionUsuario.Setup(usuarioData => usuarioData.FindOneAndReplaceAsync(
            It.IsAny<FilterDefinition<UsuarioData>>(),
            It.IsAny<UsuarioData>(),
            It.IsAny<FindOneAndReplaceOptions<UsuarioData>>(),
            It.IsAny<CancellationToken>()));

        _mockContext.Setup(context => context.Usuarios).Returns(_mockColeccionUsuario.Object);

        var usuarioRepository = new UsuarioRepositoryAdapter(_mockContext.Object);

        var usuarioActualizado = await usuarioRepository.ActualizarPorIdAsync(usuario.Id, usuarioActualizar);

        Assert.NotNull(usuarioActualizado);
        Assert.Equal(usuarioActualizar.Id, usuarioActualizado.Id);
    }

    #region Private Methods

    private static List<UsuarioData> ListarUsuariosTest() => new()
    {
        new UsuarioDataBuilderTest()
            .SetId("12345678")
            .SetNombre("Juan")
            .SetApellido("Cano")
            .SetCedula("8956735")
            .SetCorreo("juan@gmail.com")
            .SetEdad(26)
            .SetProfesion("Desarrollador")
            .Build(),
        new UsuarioDataBuilderTest()
            .SetId("10456783")
            .SetNombre("catalina")
            .SetApellido("Alvarez")
            .SetCedula("152436734")
            .SetCorreo("cata@gmail.com")
            .SetEdad(25)
            .SetProfesion("Desarrolladora")
            .Build()
    };

    #endregion Private Methods
}