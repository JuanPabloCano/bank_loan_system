using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Cuentas;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
///  Adaptador de entidad Cuenta
/// </summary>
public class CuentaRepositoryAdapter : ICuentaRepository
{
    private readonly IMongoCollection<CuentaData> _mongoCuentaCollection;

    /// <summary>
    /// Crea una instancia del repositorio <see cref="CuentaRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    public CuentaRepositoryAdapter(IContext mongoDb)
    {
        _mongoCuentaCollection = mongoDb.Cuentas;
    }

    /// <summary>
    /// <see cref="ICuentaRepository.CrearAsync"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Cuenta> CrearAsync(Cuenta entity)
    {
        CuentaData cuentaData = new(entity.UsuarioId,
            new Banco(entity.Banco.Nombre, entity.Banco.Ciudad, entity.Banco.Direccion), entity.CapacidadEndeudamiento);
        await _mongoCuentaCollection.InsertOneAsync(cuentaData);
        return cuentaData.AsEntity();
    }

    /// <summary>
    /// <see cref="ICuentaRepository.ListarTodoAsync"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Cuenta>> ListarTodoAsync()
    {
        var cuentaData = await _mongoCuentaCollection.FindAsync(Builders<CuentaData>.Filter.Empty);
        return cuentaData.ToEnumerable().Select(cuentadata => cuentadata.AsEntity()).ToList();
    }

    /// <summary>
    /// <see cref="ICuentaRepository.ObtenerEntidadPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Cuenta> ObtenerEntidadPorIdAsync(string entityId)
    {
        var cuenta = await _mongoCuentaCollection.FindAsync(data => data.Id == entityId);
        return cuenta.FirstAsync().Result.AsEntity();
    }

    public Task EliminarAsync(string entityId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Cuenta> ActualizarPorIdAsync(string entityId, Cuenta entity)
    {
        throw new System.NotImplementedException();
    }
}