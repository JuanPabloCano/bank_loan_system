using Domain.Model.Entities.Usuario;
using Domain.Model.Tests.Builders;

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
}