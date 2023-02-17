using DrivenAdapters.Mongo.Entities.Usuarios;

namespace DrivenAdapters.Mongo.Tests.Builders;

public class UsuarioDataBuilderTest
{
    private static string _id = string.Empty;
    private static string _nombre = string.Empty;
    private static string _apellido = string.Empty;
    private static string _cedula = string.Empty;
    private static string _correo = string.Empty;
    private static int _edad = 0;
    private static string _profesion = string.Empty;

    public UsuarioDataBuilderTest()
    {
    }

    public UsuarioDataBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public UsuarioDataBuilderTest SetNombre(string nombre)
    {
        _nombre = nombre;
        return this;
    }

    public UsuarioDataBuilderTest SetApellido(string apellido)
    {
        _apellido = apellido;
        return this;
    }

    public UsuarioDataBuilderTest SetCedula(string cedula)
    {
        _cedula = cedula;
        return this;
    }

    public UsuarioDataBuilderTest SetCorreo(string correo)
    {
        _correo = correo;
        return this;
    }

    public UsuarioDataBuilderTest SetEdad(int edad)
    {
        _edad = edad;
        return this;
    }

    public UsuarioDataBuilderTest SetProfesion(string profesion)
    {
        _profesion = profesion;
        return this;
    }

    public UsuarioData Build() => new(_id, _nombre, _apellido, _cedula, _correo, _edad, _profesion);
}