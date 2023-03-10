using System.Threading.Tasks;
using Domain.Model.Entities.Gateway.Base;

namespace Domain.Model.Entities.Gateway;

/// <summary>
/// Interface de repositorio de entidad <see cref="Cuenta"/>
/// </summary>
public interface ICuentaRepository : IBaseRepository<Cuenta.Cuenta, string>
{
}