using credinet.exception.middleware.models;
using Domain.Model.Entities.Usuario;

namespace Helpers.Commons.Exceptions;

/// <summary>
/// Maneja errores cuando la entidad no ha sido encontrada
/// </summary>
public abstract class EntidadNoEncontrada<TEntity>
{
    /// <summary>
    /// Error entidad no encontrada
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="message"></param>
    /// <param name="code"></param>
    /// <exception cref="BusinessException"></exception>
    public static void Apply(TEntity entity, string message, int code)
    {
        if (entity is null)
        {
            throw new BusinessException(message, code);
        }
    }
}