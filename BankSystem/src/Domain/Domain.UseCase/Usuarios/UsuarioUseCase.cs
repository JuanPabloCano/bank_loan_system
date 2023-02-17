using System.Collections.Generic;
using System.Threading.Tasks;
using credinet.exception.middleware.models;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuario;
using Domain.Model.Interfaces.UseCaseCommons;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;

namespace Domain.UseCase.Usuarios;

/// <summary>
/// Caso de uso para entidad Usuario
/// </summary>
public class UsuarioUseCase : IUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    /// <summary>
    /// Inicializa una instancia del caso de uso de la entidad <see cref="Usuario"/>
    /// </summary>
    /// <param name="usuarioRepository"></param>
    public UsuarioUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// <see cref="IUsuarioUseCase.Crear"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<Usuario> Crear(Usuario entity)
    {
        EntidadNoEncontrada<Usuario>.Apply(entity, TipoExcepcionNegocio.ErrorEntidadInvalida.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadInvalida);

        EdadEntidadInvalida(entity, TipoExcepcionNegocio.ErrorEdadEntidadIlegal.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEdadEntidadIlegal);

        entity.EstablecerCorreo(entity.Correo);
        entity.EstablecerProfesion(entity.Profesion);
        return await _usuarioRepository.CrearAsync(entity);
    }

    /// <summary>
    /// <see cref="IUsuarioUseCase.ListarTodo"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Usuario>> ListarTodo() => await _usuarioRepository.ListarTodoAsync();

    /// <summary>
    /// <see cref="IUsuarioUseCase.ObtenerEntidadPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Usuario> ObtenerEntidadPorId(string entityId)
    {
        var usuarioSeleccionado = await _usuarioRepository.ObtenerEntidadPorIdAsync(entityId);

        EntidadNoEncontrada<Usuario>.Apply(usuarioSeleccionado,
            TipoExcepcionNegocio.ErrorEntidadNoEncontrada.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadNoEncontrada);
        return usuarioSeleccionado;
    }

    /// <summary>
    /// <see cref="IUsuarioUseCase.ActualizarPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Usuario> ActualizarPorId(string entityId, Usuario entity)
    {
        EntidadNoEncontrada<Usuario>.Apply(entity,
            TipoExcepcionNegocio.ErrorEntidadNoEncontrada.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadNoEncontrada);

        EdadEntidadInvalida(entity, TipoExcepcionNegocio.ErrorEdadEntidadIlegal.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEdadEntidadIlegal);

        return await _usuarioRepository.ActualizarPorIdAsync(entityId, entity);
    }

    /// <summary>
    /// <see cref="IDelete{TEntityId}.EliminarPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    public async Task EliminarPorId(string entityId)
    {
        var usuarioSeleccionado = await ObtenerEntidadPorId(entityId);

        EntidadNoEncontrada<Usuario>.Apply(usuarioSeleccionado,
            TipoExcepcionNegocio.ErrorEntidadNoEncontrada.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadNoEncontrada);

        await _usuarioRepository.EliminarAsync(usuarioSeleccionado.Id);
    }

    private static void EdadEntidadInvalida(Usuario entity, string message, int code)
    {
        if (entity.Edad <= 18)
        {
            throw new BusinessException(message, code);
        }
    }
}

//TODO Corregir la búsqueda por id cuando no se encuentre