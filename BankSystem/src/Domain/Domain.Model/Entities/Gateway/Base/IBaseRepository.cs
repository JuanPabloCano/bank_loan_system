using Domain.Model.Interfaces.RepositoryCommons;

namespace Domain.Model.Entities.Gateway.Base;

/// <summary>
/// Interface base de repositorio con metodos genericos de CRUD
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IBaseRepository<TEntity, in TEntityId> : ICreateAsync<TEntity>, IGetAsync<TEntity, TEntityId>,
    IDeleteAsync<TEntityId>, IUpdateAsync<TEntity, TEntityId>
{
}