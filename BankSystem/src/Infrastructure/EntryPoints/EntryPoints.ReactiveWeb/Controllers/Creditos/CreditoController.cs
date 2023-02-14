using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities.Credito;
using Domain.UseCase.Common;
using Domain.UseCase.Creditos;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Creditos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers.Creditos;

/// <summary>
/// Endpoint de clase <see cref="Credito"/>
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]/[action]")]
public class CreditoController : AppControllerBase<CreditoController>
{
    private readonly ICreditoUseCase _creditoUseCase;

    /// <summary>
    /// Inicializa una instancia de la clase <see cref="CreditoController"/>
    /// </summary>
    /// <param name="eventsService"></param>
    /// <param name="creditoUseCase"></param>
    public CreditoController(IManageEventsUseCase eventsService, ICreditoUseCase creditoUseCase) : base(eventsService)
    {
        _creditoUseCase = creditoUseCase;
    }

    /// <summary>
    /// Endpoint para crear una entidad de tipo <see cref="Credito"/>
    /// </summary>
    /// <param name="creditoRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CrearCredito([FromBody] CreditoRequest creditoRequest)
    {
        return await HandleRequest(async () =>
        {
            var credito = creditoRequest.AsEntity();
            return await _creditoUseCase.Crear(credito);
        }, "");
    }

    /// <summary>
    /// Endpoint que retorna todas las entidad de tipo <see cref="Credito"/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarCreditos() =>
        await HandleRequest(async () => await _creditoUseCase.ListarTodo(), "");

    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Credito"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerCreditoPorId([FromQuery] string id) =>
        await HandleRequest(async () => await _creditoUseCase.ObtenerEntidadPorId(id), "");
    
    /// <summary>
    /// Endpoint que actualiza una entidad de tipo <see cref="Credito"/> por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="creditoRequest"></param>
    /// <returns></returns>
    [HttpPatch("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ActualizarCreditoPorId([FromQuery] string id,
        [FromBody] CreditoRequest creditoRequest)
    {
        return await HandleRequest(async () =>
        {
            var credito = creditoRequest.AsEntity();
            await _creditoUseCase.ActualizarPorId(id, credito);
            return CreditoResponse.Exec(id, credito);
        }, "");
    }
    
    /// <summary>
    /// Endpoint que elimina una entidad de tipo <see cref="Credito"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("id")]
    [ProducesResponseType(400, Type = typeof(IEnumerable<Credito>))]
    public async Task<IActionResult> EliminarCreditoPorId([FromQuery] string id)
    {
        return await HandleRequest(async () =>
        {
            await _creditoUseCase.EliminarPorId(id);
            return new
            {
                status = NoContent(),
                message = "Credito eliminado satisfactoriamente"
            };
        }, "");
    }

}