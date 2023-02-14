using Domain.Model.Interfaces.RepositoryCommons;

namespace Domain.Model.Entities.Gateway.Base;

/// <summary>
/// Interface base de repositorio con metodos de crear y listar
/// </summary>
public interface IBaseCrearListarRepository<TEntity, in TEntityId> : ICreateAsync<TEntity>,
    IGetAsync<TEntity, TEntityId>
{
}