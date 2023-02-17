using System.Collections.Generic;
using System.Threading.Tasks;
using credinet.exception.middleware.models;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;

namespace Domain.UseCase.Creditos;

/// <summary>
/// Caso de uso para entidad <see cref="Credito"/>
/// </summary>
public class CreditoUseCase : ICreditoUseCase
{
    private readonly ICreditoRepository _creditoRepository;
    private readonly ICuentaRepository _cuentaRepository;

    /// <summary>
    /// Inicializa una instancia de la clase <see cref="CreditoUseCase"/>
    /// </summary>
    /// <param name="creditoRepository"></param>
    /// <param name="cuentaRepository"></param>
    public CreditoUseCase(ICreditoRepository creditoRepository, ICuentaRepository cuentaRepository)
    {
        _creditoRepository = creditoRepository;
        _cuentaRepository = cuentaRepository;
    }

    /// <summary>
    /// <see cref="ICreditoUseCase.Crear"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Credito> Crear(Credito entity)
    {
        EntidadNoEncontrada<Credito>.Apply(entity,
            TipoExcepcionNegocio.ErrorEntidadInvalida.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadInvalida);

        await ValidarDatosDeCuenta(entity);

        return await _creditoRepository.CrearAsync(entity);
    }

    /// <summary>
    /// <see cref="ICreditoUseCase.ListarTodo"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Credito>> ListarTodo() => await _creditoRepository.ListarTodoAsync();

    /// <summary>
    /// <see cref="ICreditoUseCase.ObtenerEntidadPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Credito> ObtenerEntidadPorId(string entityId) =>
        await _creditoRepository.ObtenerEntidadPorIdAsync(entityId);

    /// <summary>
    /// <see cref="ICreditoUseCase.ActualizarPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Credito> ActualizarPorId(string entityId, Credito entity)
    {
        EntidadNoEncontrada<Credito>.Apply(entity,
            TipoExcepcionNegocio.ErrorEntidadNoEncontrada.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadNoEncontrada);
        return await _creditoRepository.ActualizarPorIdAsync(entityId, entity);
    }

    /// <summary>
    /// <see cref="ICreditoUseCase.EliminarPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    public async Task EliminarPorId(string entityId)
    {
        var creditoSeleccionado = await ObtenerEntidadPorId(entityId);
        EntidadNoEncontrada<Credito>.Apply(creditoSeleccionado,
            TipoExcepcionNegocio.ErrorEntidadNoEncontrada.GetDescription(),
            (int)TipoExcepcionNegocio.ErrorEntidadNoEncontrada);
        await _creditoRepository.EliminarAsync(entityId);
    }

    private async Task ValidarDatosDeCuenta(Credito entity)
    {
        var cuentaSeleccionada = await _cuentaRepository.ObtenerEntidadPorIdAsync(entity.CuentaId);
        if (cuentaSeleccionada.CapacidadEndeudamiento < entity.TotalPrestamo)
        {
            throw new BusinessException(
                TipoExcepcionNegocio.ErrorCapacidadEndeudamientoInvalida.GetDescription(),
                (int)TipoExcepcionNegocio.ErrorCapacidadEndeudamientoInvalida);
        }

        await DisminuirCapacidadEndeudamientoDeCuenta(entity, cuentaSeleccionada);
    }

    private async Task DisminuirCapacidadEndeudamientoDeCuenta(Credito entity, Cuenta cuentaSeleccionada)
    {
        cuentaSeleccionada.DisminuirCapacidadEndeudamiento(entity.TotalPrestamo);

        await _cuentaRepository.ActualizarPorIdAsync(cuentaSeleccionada.Id, cuentaSeleccionada);
    }
}