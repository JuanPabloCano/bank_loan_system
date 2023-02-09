using System;

namespace Helpers.Commons.Exceptions;

/// <summary>
/// Responde error cuando no se encuentra una entidad
/// </summary>
public class TipoExcepcionEntidadNoEncontrada : Exception
{
    /// <summary>
    /// Error
    /// </summary>
    public object Error { get; set; }

    /// <summary>
    /// Crea una instancia y devuelve un error
    /// </summary>
    /// <param name="message"></param>
    /// <param name="error"></param>
    public TipoExcepcionEntidadNoEncontrada(string message, object error = null) : base(message)
    {
        Error = error;
    }
}