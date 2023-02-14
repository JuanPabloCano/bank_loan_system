using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Pago;
using Domain.Model.ValueObjects;

namespace Domain.UseCase.Pagos;

/// <summary>
/// Caso de uso para entidad <see cref="Pago"/>
/// </summary>
public class PagoUseCase : IPagoUseCase
{
    private readonly IPagoRepository _pagoRepository;
    private readonly ICreditoRepository _creditoRepository;

    /// <summary>
    /// Inicializa una instancia de la clase <see cref="PagoUseCase"/>
    /// </summary>
    /// <param name="pagoRepository"></param>
    /// <param name="creditoRepository"></param>
    public PagoUseCase(IPagoRepository pagoRepository, ICreditoRepository creditoRepository)
    {
        _pagoRepository = pagoRepository;
        _creditoRepository = creditoRepository;
    }

    /// <summary>
    /// <see cref="IPagoUseCase.Crear"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Pago> Crear(Pago entity)
    {
        var creditoAPagar = await _creditoRepository.ObtenerEntidadPorIdAsync(entity.CreditoId);
        creditoAPagar.RealizarPago(entity.Cantidad);
        await _creditoRepository.ActualizarPorIdAsync(creditoAPagar.Id, creditoAPagar);
        entity.FechaPago = new FechaPago(DateTime.Now);
        return await _pagoRepository.CrearAsync(entity);
    }

    /// <summary>
    /// <see cref="IPagoUseCase.ListarTodo"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Pago>> ListarTodo() => await _pagoRepository.ListarTodoAsync();

    /// <summary>
    /// <see cref="IPagoUseCase.ObtenerEntidadPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Pago> ObtenerEntidadPorId(string entityId) =>
        await _pagoRepository.ObtenerEntidadPorIdAsync(entityId);
}