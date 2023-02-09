using System.Threading.Tasks;

namespace Domain.Model.Interfaces.RepositoryCommons;

/// <summary>
/// Interface de repositorio generica para actualizar una entidad
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IUpdateAsync<TEntity, in TEntityId>
{
    /// <summary>
    /// Metodo de repositorio para actualizar una entidad por Id
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> ActualizarPorIdAsync(TEntityId entityId, TEntity entity);
}