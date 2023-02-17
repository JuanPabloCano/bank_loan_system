using Domain.Model.Entities.Usuario;

namespace Domain.Model.Tests.Builders;

public class UsuarioBuilderTest
{
    private static string _id = string.Empty;
    private static string _nombre = string.Empty;
    private static string _apellido = string.Empty;
    private static string _cedula = string.Empty;
    private static string _correo = string.Empty;
    private static int _edad = 0;
    private static string _profesion = string.Empty;

    public UsuarioBuilderTest()
    {
    }

    public UsuarioBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public UsuarioBuilderTest SetNombre(string nombre)
    {
        _nombre = nombre;
        return this;
    }

    public UsuarioBuilderTest SetApellido(string apellido)
    {
        _apellido = apellido;
        return this;
    }

    public UsuarioBuilderTest SetCedula(string cedula)
    {
        _cedula = cedula;
        return this;
    }

    public UsuarioBuilderTest SetCorreo(string correo)
    {
        _correo = correo;
        return this;
    }

    public UsuarioBuilderTest SetEdad(int edad)
    {
        _edad = edad;
        return this;
    }

    public UsuarioBuilderTest SetProfesion(string profesion)
    {
        _profesion = profesion;
        return this;
    }

    public Usuario Build() => new(_id, _nombre, _apellido, _cedula, _correo, _edad, _profesion);
}