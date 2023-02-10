using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuario;
using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Cuentas;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
///  Adaptador de entidad Cuenta
/// </summary>
public class CuentaRepositoryAdapter : ICuentaRepository
{
    private readonly IMongoCollection<CuentaData> _mongoCuentaCollection;
    private readonly IUsuarioRepository _usuarioRepository;

    /// <summary>
    /// Crea una instancia del repositorio <see cref="CuentaRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    /// <param name="usuarioRepository"></param>
    public CuentaRepositoryAdapter(IContext mongoDb, IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
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
        var cuentaData = await _mongoCuentaCollection.FindAsync(data => data.Id == entityId);
        var cuenta = cuentaData.FirstAsync().Result.AsEntity();
        var usuario = await _usuarioRepository.ObtenerEntidadPorIdAsync(cuenta.UsuarioId);

        Usuario usuarioSeleccionado = new(usuario.Id, usuario.Nombre, usuario.Apellido, usuario.Cedula, usuario.Correo,
            usuario.Edad, usuario.Profesion);

        cuenta.Usuario = usuarioSeleccionado;

        return cuenta;
    }

    /// <summary>
    /// <see cref="ICuentaRepository.EliminarAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    public async Task EliminarAsync(string entityId) =>
        await _mongoCuentaCollection.FindOneAndDeleteAsync(cuenta => cuenta.Id == entityId);

    /// <summary>
    /// <see cref="ICuentaRepository.ActualizarPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Cuenta> ActualizarPorIdAsync(string entityId, Cuenta entity)
    {
        CuentaData cuentaData = new(entityId, entity.UsuarioId, entity.Banco, entity.CapacidadEndeudamiento);
        var cuentaActualizada =
            await _mongoCuentaCollection.FindOneAndReplaceAsync(cuenta => cuenta.Id == entityId, cuentaData);

        return cuentaActualizada.AsEntity();
    }
}