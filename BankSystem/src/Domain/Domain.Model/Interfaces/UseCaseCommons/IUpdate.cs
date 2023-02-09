using System.Threading.Tasks;

namespace Domain.Model.Interfaces.UseCaseCommons;

/// <summary>
/// Interface generica para actualizar una entidad
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IUpdate<TEntity, in TEntityId>
{
    /// <summary>
    /// Metodo para actualizar una entidad por Id
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> ActualizarPorId(TEntityId entityId, TEntity entity);
}