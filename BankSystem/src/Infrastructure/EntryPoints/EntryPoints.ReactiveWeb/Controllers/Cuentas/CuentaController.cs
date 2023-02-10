using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities.Cuenta;
using Domain.UseCase.Common;
using Domain.UseCase.Cuentas;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Cuentas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers.Cuentas;

/// <summary>
/// EntryPoint de entidad <see cref="Cuenta"/>
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]/[action]")]
public class CuentaController : AppControllerBase<CuentaController>
{
    private readonly ICuentaUseCase _cuentaUseCase;

    /// <summary>
    /// Inicializa una instancia de <see cref="CuentaController"/>
    /// </summary>
    /// <param name="eventsService"></param>
    /// <param name="cuentaUseCase"></param>
    public CuentaController(IManageEventsUseCase eventsService, ICuentaUseCase cuentaUseCase) : base(eventsService)
    {
        _cuentaUseCase = cuentaUseCase;
    }

    /// <summary>
    /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
    /// </summary>
    /// <param name="cuentaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CrearCuenta([FromBody] CuentaRequest cuentaRequest)
    {
        return await HandleRequest(async () =>
        {
            var cuenta = cuentaRequest.AsEntity();
            return await _cuentaUseCase.Crear(cuenta);
        }, "");
    }

    /// <summary>
    /// Endpoint que retorna todas las entidad de tipo <see cref="Cuenta"/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarCuentas() =>
        await HandleRequest(async () => await _cuentaUseCase.ListarTodo(), "");

    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Cuenta"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerCuentaPorId([FromQuery] string id) =>
        await HandleRequest(async () => await _cuentaUseCase.ObtenerEntidadPorId(id), "");

    /// <summary>
    /// Endpoint que actualiza una entidad de tipo <see cref="Cuenta"/> por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cuentaRequest"></param>
    /// <returns></returns>
    [HttpPatch("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ActualizarCuentaPorId([FromQuery] string id,
        [FromBody] CuentaRequest cuentaRequest)
    {
        return await HandleRequest(async () =>
        {
            var cuenta = cuentaRequest.AsEntity();
            await _cuentaUseCase.ActualizarPorId(id, cuenta);
            return CuentaResponse.Exec(id, cuenta);
        }, "");
    }


    /// <summary>
    /// Endpoint que elimina una entidad de tipo <see cref="Cuenta"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("id")]
    [ProducesResponseType(400, Type = typeof(IEnumerable<Cuenta>))]
    public async Task<IActionResult> EliminarCuentaPorId([FromQuery] string id)
    {
        return await HandleRequest(async () =>
        {
            await _cuentaUseCase.EliminarPorId(id);
            return new
            {
                status = NoContent(),
                message = "Cuenta eliminada satisfactoriamente"
            };
        }, "");
    }
}