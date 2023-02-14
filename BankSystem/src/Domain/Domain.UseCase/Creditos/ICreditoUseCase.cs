using System.Threading.Tasks;
using Domain.Model.Entities.Credito;
using Domain.UseCase.Base;

namespace Domain.UseCase.Creditos;

/// <summary>
/// Interface de entidad <see cref="Credito"/>
/// </summary>
public interface ICreditoUseCase : IBaseUseCase<Credito, string>
{
    /// <summary>
    /// Metodo para realizar pago de credito
    /// </summary>
    /// <param name="creditoId"></param>
    /// <param name="cantidad"></param>
    /// <returns></returns>
    Task<Credito> RealizarPagoDeCredito(string creditoId, int cantidad);
}