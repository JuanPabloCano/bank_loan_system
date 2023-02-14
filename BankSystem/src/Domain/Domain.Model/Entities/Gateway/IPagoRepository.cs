using Domain.Model.Entities.Gateway.Base;

namespace Domain.Model.Entities.Gateway;

/// <summary>
/// Interface de repositorio de entidad <see cref="Pago"/>
/// </summary>
public interface IPagoRepository : IBaseCrearListarRepository<Pago.Pago, string>
{
}