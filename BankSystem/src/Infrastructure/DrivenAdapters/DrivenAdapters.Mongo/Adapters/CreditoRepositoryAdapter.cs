using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities.Creditos;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
/// Adaptador de entidad <see cref="Credito"/>
/// </summary>
public class CreditoRepositoryAdapter : ICreditoRepository
{
    private readonly IMongoCollection<CreditoData> _mongoCreditoCollection;

    /// <summary>
    /// Crea una instancia del repositorio <see cref="CreditoRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    public CreditoRepositoryAdapter(IContext mongoDb)
    {
        _mongoCreditoCollection = mongoDb.Creditos;
    }

    /// <summary>
    /// <see cref="ICreditoRepository.CrearAsync"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Credito> CrearAsync(Credito entity)
    {
        CreditoData creditoData = new(entity.UsuarioId, entity.CuentaId, entity.TotalPrestamo, entity.DeudaActual);
        await _mongoCreditoCollection.InsertOneAsync(creditoData);
        return creditoData.AsEntity();
    }

    /// <summary>
    /// <see cref="ICreditoRepository.ListarTodoAsync"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Credito>> ListarTodoAsync()
    {
        var creditoData = await _mongoCreditoCollection.FindAsync(Builders<CreditoData>.Filter.Empty);
        return creditoData.ToEnumerable().Select(creditodata => creditodata.AsEntity()).ToList();
    }

    /// <summary>
    /// <see cref="ICreditoRepository.ObtenerEntidadPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Credito> ObtenerEntidadPorIdAsync(string entityId)
    {
        var creditoData = await _mongoCreditoCollection.FindAsync(data => data.Id == entityId);
        return creditoData.FirstAsync().Result.AsEntity();
    }

    /// <summary>
    /// <see cref="ICreditoRepository.EliminarAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    public async Task EliminarAsync(string entityId) =>
        await _mongoCreditoCollection.FindOneAndDeleteAsync(credito => credito.Id == entityId);

    /// <summary>
    /// <see cref="ICreditoRepository.ActualizarPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Credito> ActualizarPorIdAsync(string entityId, Credito entity)
    {
        CreditoData creditoData = new(entityId, entity.UsuarioId, entity.CuentaId, entity.TotalPrestamo,
            entity.DeudaActual);
        var creditoActualizado =
            await _mongoCreditoCollection.FindOneAndReplaceAsync(credito => credito.Id == entityId, creditoData);
        return creditoActualizado.AsEntity();
    }

    public async Task<Credito> RealizarPagoDeCreditoAsync(string creditoId, int cantidad)
    {
        throw new System.NotImplementedException();
    }
}