using System.Threading.Tasks;

namespace Domain.Model.Interfaces.UseCaseCommons;

/// <summary>
/// Interface generica para eliminar entidades
/// </summary>
/// <typeparam name="TEntityId"></typeparam>
public interface IDelete<in TEntityId>
{
    /// <summary>
    /// Metodo para eliminar una entidad por Id
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task EliminarPorId(TEntityId entityId);
}