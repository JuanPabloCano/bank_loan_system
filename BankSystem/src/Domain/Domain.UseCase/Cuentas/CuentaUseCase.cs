﻿using System.Collections.Generic;
using System.Threading.Tasks;
using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Gateway;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;

namespace Domain.UseCase.Cuentas;

/// <summary>
/// Caso de uso para entidad Cuenta
/// </summary>
public class CuentaUseCase : ICuentaUseCase
{
    private readonly ICuentaRepository _cuentaRepository;

    /// <summary>
    /// Inicializa una instancia del caso de uso de la entidad <see cref="Cuenta"/>
    /// </summary>
    /// <param name="cuentaRepository"></param>
    public CuentaUseCase(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    /// <summary>
    /// <see cref="ICuentaUseCase.Crear"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<Cuenta> Crear(Cuenta entity)
    {
        if (entity.CapacidadEndeudamiento == 0)
        {
            throw new BusinessException(TipoExcepcionNegocio.ErrorCapacidadEndeudamientoInvalida.GetDescription(),
                (int)TipoExcepcionNegocio.ErrorCapacidadEndeudamientoInvalida);
        }

        return await _cuentaRepository.CrearAsync(entity);
    }

    /// <summary>
    /// <see cref="ICuentaUseCase.ListarTodo"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Cuenta>> ListarTodo() => await _cuentaRepository.ListarTodoAsync();

    /// <summary>
    /// <see cref="ICuentaUseCase.ObtenerEntidadPorId"/>
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<Cuenta> ObtenerEntidadPorId(string entityId) =>
        await _cuentaRepository.ObtenerEntidadPorIdAsync(entityId);

    public Task<Cuenta> ActualizarPorId(string entityId, Cuenta entity)
    {
        throw new System.NotImplementedException();
    }

    public Task EliminarPorId(string entityId)
    {
        throw new System.NotImplementedException();
    }
}