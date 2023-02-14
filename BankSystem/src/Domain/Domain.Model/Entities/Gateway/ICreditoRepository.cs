using System.Threading.Tasks;
using Domain.Model.Entities.Gateway.Base;

namespace Domain.Model.Entities.Gateway;

/// <summary>
/// Interface de repositorio de entidad <see cref="Credito"/>
/// </summary>
public interface ICreditoRepository : IBaseRepository<Credito.Credito, string>
{
    /// <summary>
    /// Metodo de repositorio para relizar pago de credito
    /// </summary>
    /// <param name="creditoId"></param>
    /// <param name="cantidad"></param>
    /// <returns></returns>
    Task<Credito.Credito> RealizarPagoDeCreditoAsync(string creditoId, int cantidad);
}