using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuario;
using DrivenAdapters.Mongo.Entities.Usuarios;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
/// Adaptador de entidad Usuario
/// </summary>
public class UsuarioRepositoryAdapter : IUsuarioRepository
{
    private readonly IMongoCollection<UsuarioData> _mongoUsuarioCollection;

    /// <summary>
    /// Crea una instancia del repositorio <see cref="UsuarioRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    public UsuarioRepositoryAdapter(IContext mongoDb)
    {
        _mongoUsuarioCollection = mongoDb.Usuarios;
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.CrearAsync"/>
    /// Crea un Usuario
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Usuario> CrearAsync(Usuario entity)
    {
        UsuarioData usuarioData = new(entity.Nombre, entity.Apellido, entity.Cedula, entity.Correo, entity.Edad,
            entity.Profesion);
        await _mongoUsuarioCollection.InsertOneAsync(usuarioData);
        return usuarioData.AsEntity();
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.ListarTodoAsync"/>
    /// Retorna lista de Usuarios
    /// </summary>
    /// <returns></returns>
    public async Task<List<Usuario>> ListarTodoAsync()
    {
        var usuarioData = await _mongoUsuarioCollection.FindAsync(Builders<UsuarioData>.Filter.Empty);
        return usuarioData.ToEnumerable().Select(userdata => userdata.AsEntity()).ToList();
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.ObtenerEntidadPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Usuario> ObtenerEntidadPorIdAsync(string entityId)
    {
        var usuario = await _mongoUsuarioCollection.FindAsync(user => user.Id == entityId);
        return usuario.FirstAsync().Result.AsEntity();
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.EliminarAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    public async Task EliminarAsync(string entityId) =>
        await _mongoUsuarioCollection.FindOneAndDeleteAsync(user => user.Id == entityId);

    /// <summary>
    /// <see cref="IUsuarioRepository.ActualizarPorIdAsync"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Usuario> ActualizarPorIdAsync(string entityId, Usuario entity)
    {
        UsuarioData usuarioData = new(entityId, entity.Nombre, entity.Apellido, entity.Cedula, entity.Correo,
            entity.Edad,
            entity.Profesion);
        var usuarioActualizado = await _mongoUsuarioCollection.FindOneAndReplaceAsync(
            selectedUser => selectedUser.Id == entityId,
            usuarioData);
        return usuarioActualizado.AsEntity();
    }
}