using Domain.Model.Entities.Usuario;

namespace EntryPoints.ReactiveWeb.Entities.Usuarios;

/// <summary>
/// Clase de respuesta de entidad Usuario
/// </summary>
public class UsuarioResponse
{
    /// <summary>
    /// Metodo que retorna un Usuario
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuario"></param>
    /// <returns></returns>
    public static object Exec(string id, Usuario usuario) => new
    {
        Id = id,
        usuario.Nombre,
        usuario.Apellido,
        usuario.Cedula,
        usuario.Correo,
        usuario.Edad,
        usuario.Profesion
    };
}