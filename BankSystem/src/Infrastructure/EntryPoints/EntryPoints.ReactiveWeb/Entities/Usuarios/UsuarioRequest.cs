using Domain.Model.Entities.Usuario;

namespace EntryPoints.ReactiveWeb.Entities.Usuarios;

/// <summary>
/// Request DTO de entidad <see cref="Usuario"/>
/// </summary>
public class UsuarioRequest
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
    public string Correo { get; set; }

    /// <summary>
    /// Edad
    /// </summary>
    public int Edad { get; set; }

    /// <summary>
    /// Profesion
    /// </summary>
    public string Profesion { get; set; }

    /// <summary>
    /// Mapper de entidad <see cref="Usuario"/>
    /// </summary>
    /// <returns></returns>
    public Usuario AsEntity() => new(Id, Nombre, Apellido, Cedula, Correo, Edad, Profesion);
}