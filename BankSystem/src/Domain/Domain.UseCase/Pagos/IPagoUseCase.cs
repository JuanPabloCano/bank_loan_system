using Domain.Model.Entities.Pago;
using Domain.UseCase.Base;

namespace Domain.UseCase.Pagos;

/// <summary>
/// Interface de entidad <see cref="Pago"/>
/// </summary>
public interface IPagoUseCase : IBaseCrearListarUseCase<Pago, string>
{
}