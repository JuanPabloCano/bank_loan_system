using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Pago;
using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Pagos;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
///  Adaptador de entidad <see cref="Pago"/>
/// </summary>
public class PagoRepositoryAdapter : IPagoRepository
{
    private readonly IMongoCollection<PagoData> _mongoPagoCollection;

    /// <summary>
    /// Crea una instancia del repositorio <see cref="PagoRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    public PagoRepositoryAdapter(IContext mongoDb)
    {
        _mongoPagoCollection = mongoDb.Pagos;
    }

    /// <summary>
    /// <see cref="IPagoRepository.CrearAsync"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Pago> CrearAsync(Pago entity)
    {
        PagoData pagoData = new(entity.UsuarioId, entity.CreditoId, entity.Cantidad,
            new FechaPago(DateTime.Now));
        await _mongoPagoCollection.InsertOneAsync(pagoData);
        return pagoData.AsEntity();
    }

    /// <summary>
    /// <see cref="IPagoRepository.ListarTodoAsync"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Pago>> ListarTodoAsync()
    {
        var pagoData = await _mongoPagoCollection.FindAsync(Builders<PagoData>.Filter.Empty);
        return pagoData.ToEnumerable().Select(pagodata => pagodata.AsEntity()).ToList();
    }

    /// <summary>
    /// <see cref="IPagoRepository.ObtenerEntidadPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Pago> ObtenerEntidadPorIdAsync(string entityId)
    {
        var pagoData = await _mongoPagoCollection.FindAsync(pagodata => pagodata.Id == entityId);
        return pagoData.FirstAsync().Result.AsEntity();
    }
}