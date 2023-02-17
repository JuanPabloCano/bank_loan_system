using Domain.Model.Entities.Gateway.Base;

namespace Domain.Model.Entities.Gateway;

/// <summary>
/// Interface de repositorio de entidad <see cref="Credito"/>
/// </summary>
public interface ICreditoRepository : IBaseRepository<Credito.Credito, string>
{
}