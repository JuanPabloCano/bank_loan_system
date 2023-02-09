using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.UseCaseCommons;

/// <summary>
/// Interface generica para listar entidades
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IGet<TEntity, in TEntityId>
{
    /// <summary>
    /// Metodo para obtener todas las entidades creadas
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> ListarTodo();

    /// <summary>
    /// Metodo para obtener una entidad por id
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task<TEntity> ObtenerEntidadPorId(TEntityId entityId);
}