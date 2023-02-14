using Domain.Model.ValueObjects;

namespace Domain.Model.Entities.Pago;

/// <summary>
/// Clase de Pago
/// </summary>
public class Pago
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
    /// Crea una instancia con todos los atributos de la clase <see cref="Credito"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioId"></param>
    /// <param name="creditoId"></param>
    /// <param name="cantidad"></param>
    /// <param name="fechaPago"></param>
    public Pago(string id, string usuarioId, string creditoId, int cantidad, FechaPago fechaPago)
    {
        Id = id;
        UsuarioId = usuarioId;
        CreditoId = creditoId;
        Cantidad = cantidad;
        FechaPago = fechaPago;
    }
}