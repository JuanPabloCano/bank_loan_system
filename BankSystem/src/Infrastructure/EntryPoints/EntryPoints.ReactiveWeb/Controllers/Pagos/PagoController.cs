using System.Threading.Tasks;
using Domain.Model.Entities.Pago;
using Domain.UseCase.Common;
using Domain.UseCase.Pagos;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Pagos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers.Pagos;

/// <summary>
/// EntryPoint de entidad <see cref="Pago"/>
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]/[action]")]
public class PagoController : AppControllerBase<PagoController>
{
    private readonly IPagoUseCase _pagoUseCase;

    /// <summary>
    /// Inicializa una instancia <see cref="PagoController"/>
    /// </summary>
    /// <param name="eventsService"></param>
    /// <param name="pagoUseCase"></param>
    public PagoController(IManageEventsUseCase eventsService, IPagoUseCase pagoUseCase) : base(eventsService)
    {
        _pagoUseCase = pagoUseCase;
    }

    /// <summary>
    /// Endopoint para crear entidad de tipo <see cref="Pago"/>
    /// </summary>
    /// <param name="pagoRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CrearPago([FromBody] PagoRequest pagoRequest)
    {
        return await HandleRequest(async () => await _pagoUseCase.Crear(pagoRequest.AsEntity()), "");
    }

    /// <summary>
    /// Endpoint que retorna todas las entidad de tipo <see cref="Pago"/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarPagos() =>
        await HandleRequest(async () => await _pagoUseCase.ListarTodo(), "");


    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Pago"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPagoPorId([FromQuery] string id) =>
        await HandleRequest(async () => await _pagoUseCase.ObtenerEntidadPorId(id), "");
}