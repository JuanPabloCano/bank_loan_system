using Domain.Model.Entities.Cuenta;
using Domain.UseCase.Base;

namespace Domain.UseCase.Cuentas;

/// <summary>
/// Interface de entidad Cuenta
/// </summary>
public interface ICuentaUseCase : IBaseUseCase<Cuenta, string>
{
}