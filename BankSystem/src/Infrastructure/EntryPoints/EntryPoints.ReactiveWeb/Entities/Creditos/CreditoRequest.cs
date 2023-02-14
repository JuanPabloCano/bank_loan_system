using Domain.Model.Entities.Credito;

namespace EntryPoints.ReactiveWeb.Entities.Creditos;

/// <summary>
/// Request DTO de entidad <see cref="Credito"/>
/// </summary>
public class CreditoRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// UsuarioId
    /// </summary>
    public string UsuarioId { get; set; }

    /// <summary>
    /// CuentaId
    /// </summary>
    public string CuentaId { get; set; }

    /// <summary>
    /// Total Prestamo
    /// </summary>
    public int TotalPrestamo { get; set; }

    /// <summary>
    /// Deuda Actual
    /// </summary>
    public int DeudaActual { get; set; }

    /// <summary>
    /// Mapper de entidad <see cref="Credito"/>
    /// </summary>
    /// <returns></returns>
    public Credito AsEntity() => new(Id, UsuarioId, CuentaId, TotalPrestamo, DeudaActual);
}