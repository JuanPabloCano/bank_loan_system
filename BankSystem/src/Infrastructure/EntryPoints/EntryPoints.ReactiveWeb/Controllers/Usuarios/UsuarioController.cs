using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities.Usuario;
using Domain.UseCase.Common;
using Domain.UseCase.Usuarios;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers.Usuarios;

/// <summary>
/// EntryPoint de entidad <see cref="Usuario"/>
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]/[action]")]
public class UsuarioController : AppControllerBase<UsuarioController>
{
    private readonly IUsuarioUseCase _usuarioUseCase;

    /// <summary>
    /// Inicializa una instancia de <see cref="UsuarioController"/>
    /// </summary>
    /// <param name="eventsService"></param>
    /// <param name="usuarioUseCase"></param>
    public UsuarioController(IManageEventsUseCase eventsService, IUsuarioUseCase usuarioUseCase) : base(eventsService)
    {
        _usuarioUseCase = usuarioUseCase;
    }


    /// <summary>
    /// Endpoint para crear entidad de tipo <see cref="Usuario"/>
    /// </summary>
    /// <param name="usuarioRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRequest usuarioRequest)
    {
        return await HandleRequest(async () =>
        {
            var usuario = usuarioRequest.AsEntity();
            return await _usuarioUseCase.Crear(usuario);
        }, "");
    }


    /// <summary>
    /// Endpoint que retorna todas las entidad de tipo <see cref="Usuario"/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarUsuarios() =>
        await HandleRequest(async () => await _usuarioUseCase.ListarTodo(), "");


    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Usuario"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerUsuarioPorId([FromQuery] string id) =>
        await HandleRequest(async () => await _usuarioUseCase.ObtenerEntidadPorId(id), "");

    /// <summary>
    /// Endpoint que actualiza una entidad de tipo <see cref="Usuario"/> por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioRequest"></param>
    /// <returns></returns>
    [HttpPatch("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ActualizarUsuarioPorId([FromQuery] string id,
        [FromBody] UsuarioRequest usuarioRequest)
    {
        return await HandleRequest(async () =>
        {
            var usuario = usuarioRequest.AsEntity();
            await _usuarioUseCase.ActualizarPorId(id, usuario);
            return UsuarioResponse.Exec(id, usuario);
        }, "");
    }

    /// <summary>
    /// Endpoint que elimina una entidad de tipo <see cref="Usuario"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("id")]
    [ProducesResponseType(400, Type = typeof(IEnumerable<Usuario>))]
    public async Task<IActionResult> EliminarUsuarioPorId([FromQuery] string id)
    {
        return await HandleRequest(async () =>
        {
            await _usuarioUseCase.EliminarPorId(id);
            return new
            {
                status = NoContent(),
                message = "Usuario eliminado satisfactoriamente"
            };
        }, "");
    }
}