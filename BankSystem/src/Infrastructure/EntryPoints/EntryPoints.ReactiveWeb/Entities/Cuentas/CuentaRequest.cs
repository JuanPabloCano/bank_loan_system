using Domain.Model.Entities.Cuenta;
using Domain.Model.ValueObjects;

namespace EntryPoints.ReactiveWeb.Entities.Cuentas;

/// <summary>
/// Request DTO de entidad <see cref="Cuenta"/>
/// </summary>
public class CuentaRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Id del Usuario
    /// </summary>
    public string UsuarioId { get; set; }

    /// <summary>
    /// Banco
    /// </summary>
    public Banco Banco { get; set; }

    /// <summary>
    /// Capacidad de endeudamiento
    /// </summary>
    public int CapacidadEndeudamiento { get; set; }

    /// <summary>
    /// Mapper de entidad <see cref="Cuenta"/>
    /// </summary>
    /// <returns></returns>
    public Cuenta AsEntity() => new(Id, UsuarioId, Banco, CapacidadEndeudamiento);
}