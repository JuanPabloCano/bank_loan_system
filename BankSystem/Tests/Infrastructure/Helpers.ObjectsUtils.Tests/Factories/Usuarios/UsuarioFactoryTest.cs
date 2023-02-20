using Domain.Model.Entities.Usuario;
using Domain.Model.Tests.Builders;
using EntryPoints.ReactiveWeb.Entities.Usuarios;

namespace Helpers.ObjectsUtils.Tests.Factories.Usuarios;

public abstract class UsuarioFactoryTest
{
    public static Usuario ObtenerUsuarioTest(string id, string nombre, string apellido, string cedula,
        string correo, int edad, string profesion) => new UsuarioBuilderTest()
        .SetId(id)
        .SetNombre(nombre)
        .SetApellido(apellido)
        .SetCedula(cedula)
        .SetCorreo(correo)
        .SetEdad(edad)
        .SetProfesion(profesion)
        .Build();

    public static UsuarioRequest ObtenerUsuarioRequestTest(string id, string nombre, string apellido, string cedula,
        string correo, int edad, string profesion) => new UsuarioRequest()
    {
        Id = id,
        Nombre = nombre,
        Apellido = apellido,
        Cedula = cedula,
        Correo = correo,
        Edad = edad,
        Profesion = profesion
    };

    public static List<Usuario> ListarUsuariosTest() => new()
    {
        new UsuarioBuilderTest()
            .SetId("12345678")
            .SetNombre("Juan")
            .SetApellido("Cano")
            .SetCedula("8956735")
            .SetCorreo("juan@gmail.com")
            .SetEdad(26)
            .SetProfesion("Desarrollador")
            .Build(),
        new UsuarioBuilderTest()
            .SetId("10456783")
            .SetNombre("catalina")
            .SetApellido("Alvarez")
            .SetCedula("152436734")
            .SetCorreo("cata@gmail.com")
            .SetEdad(25)
            .SetProfesion("Desarrolladora")
            .Build()
    };
}