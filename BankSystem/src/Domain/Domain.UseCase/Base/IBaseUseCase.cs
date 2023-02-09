using Domain.Model.Interfaces;
using Domain.Model.Interfaces.UseCaseCommons;

namespace Domain.UseCase.Base;

/// <summary>
/// Interface base con metodos genericos del CRUD
/// </summary>
public interface IBaseUseCase<TEntity, in TEntityId> : ICreate<TEntity>, IGet<TEntity, TEntityId>,
    IUpdate<TEntity, TEntityId>, IDelete<TEntityId>
{
}