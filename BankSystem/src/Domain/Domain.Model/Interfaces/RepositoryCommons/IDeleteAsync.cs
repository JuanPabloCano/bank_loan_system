using System.Threading.Tasks;

namespace Domain.Model.Interfaces.RepositoryCommons;

/// <summary>
/// Interface de repositorio generica para eliminar entidades
/// </summary>
/// <typeparam name="TEntityId"></typeparam>
public interface IDeleteAsync<in TEntityId>
{
    /// <summary>
    /// Metodo de repositorio para eliminar una entidad
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task EliminarAsync(TEntityId entityId);
}