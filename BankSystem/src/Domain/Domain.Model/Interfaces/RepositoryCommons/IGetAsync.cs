using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.RepositoryCommons;

/// <summary>
/// Interface de repositorio generica para listar entidades
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IGetAsync<TEntity, in TEntityId>
{
    /// <summary>
    /// Metodo de repositorio para obtener todas las entidades creadas
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> ListarTodoAsync();

    /// <summary>
    /// Metodo de repositorio para obtener una entidad por id
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task<TEntity> ObtenerEntidadPorIdAsync(TEntityId entityId);
}