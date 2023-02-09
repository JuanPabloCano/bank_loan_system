namespace Domain.Model.Entities.Usuario;

/// <summary>
/// Clase Usuario
/// </summary>
public class Usuario
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    public string Apellido { get; set; }

    /// <summary>
    /// Cedula
    /// </summary>
    public string Cedula { get; set; }

    /// <summary>
    /// Correo
    /// </summary>
    public string Correo { get; private set; }

    /// <summary>
    /// Edad
    /// </summary>
    public int Edad { get; set; }

    /// <summary>
    /// Profesion
    /// </summary>
    public string Profesion { get; private set; }

    /// <summary>
    /// Crea una instancia con todos los atributos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="cedula"></param>
    /// <param name="correo"></param>
    /// <param name="edad"></param>
    /// <param name="profesion"></param>
    public Usuario(string id, string nombre, string apellido, string cedula, string correo, int edad, string profesion)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Cedula = cedula;
        Correo = correo;
        Edad = edad;
        Profesion = profesion;
    }

    /// <summary>
    /// Crea una instancia sin el Id
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="cedula"></param>
    /// <param name="correo"></param>
    /// <param name="edad"></param>
    /// <param name="profesion"></param>
    public Usuario(string nombre, string apellido, string cedula, string correo, int edad, string profesion)
    {
        Nombre = nombre;
        Apellido = apellido;
        Cedula = cedula;
        Correo = correo;
        Edad = edad;
        Profesion = profesion;
    }

    /// <summary>
    /// Establece el correo
    /// </summary>
    /// <param name="correo"></param>
    public void EstablecerCorreo(string correo) => Correo = correo;

    /// <summary>
    /// Establece la profesion
    /// </summary>
    /// <param name="profesion"></param>
    public void EstablecerProfesion(string profesion) => Profesion = profesion;
}