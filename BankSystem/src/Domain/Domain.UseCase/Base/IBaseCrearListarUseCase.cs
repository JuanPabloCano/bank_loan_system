using Domain.Model.Interfaces.UseCaseCommons;

namespace Domain.UseCase.Base;

/// <summary>
/// Interface base con metodos de crear y listar
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IBaseCrearListarUseCase<TEntity, in TEntityId> : ICreate<TEntity>, IGet<TEntity, TEntityId>
{
}