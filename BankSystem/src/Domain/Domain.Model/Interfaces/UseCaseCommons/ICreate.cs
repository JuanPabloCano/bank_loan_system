using System.Threading.Tasks;

namespace Domain.Model.Interfaces.UseCaseCommons;

/// <summary>
/// Interface generica para crear entidad
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ICreate<TEntity>
{
    /// <summary>
    /// Metodo para crear
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> Crear(TEntity entity);
}