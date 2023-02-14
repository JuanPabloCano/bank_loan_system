using Domain.Model.Entities.Pago;
using Domain.Model.ValueObjects;

namespace EntryPoints.ReactiveWeb.Entities.Pagos;

/// <summary>
/// Request DTO de entidad <see cref="Pago"/>
/// </summary>
public class PagoRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Usuario Id
    /// </summary>
    public string UsuarioId { get; set; }

    /// <summary>
    /// Credito Id
    /// </summary>
    public string CreditoId { get; set; }

    /// <summary>
    /// Cantidad
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Fecha de Pago
    /// </summary>
    public FechaPago FechaPago { get; set; }

    /// <summary>
    /// Mapper de entidad <see cref="Pago"/>
    /// </summary>
    /// <returns></returns>
    public Pago AsEntity() => new(Id, UsuarioId, CreditoId, Cantidad, FechaPago);
}