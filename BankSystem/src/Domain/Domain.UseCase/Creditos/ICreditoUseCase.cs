using Domain.Model.Entities.Credito;
using Domain.UseCase.Base;

namespace Domain.UseCase.Creditos;

/// <summary>
/// Interface de entidad <see cref="Credito"/>
/// </summary>
public interface ICreditoUseCase : IBaseUseCase<Credito, string>
{
}