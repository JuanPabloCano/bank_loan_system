using System.Threading.Tasks;

namespace Domain.Model.Interfaces.RepositoryCommons;

/// <summary>
/// Interface de repositorio generica para crear entidad
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ICreateAsync<TEntity>
{
    /// <summary>
    /// Metodo de repositorio para crear entidad
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> CrearAsync(TEntity entity);
}