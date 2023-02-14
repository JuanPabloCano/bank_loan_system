using System;

namespace Domain.Model.ValueObjects;

/// <summary>
/// Objeto de valor Fecha de pago
/// </summary>
public class FechaPago
{
    /// <summary>
    /// Fecha de pago
    /// </summary>
    public DateTime FechaDePago { get; set; }

    /// <summary>
    /// Crea una instancia del objeto de valor <see cref="FechaDePago"/>
    /// </summary>
    /// <param name="fechaDePago"></param>
    public FechaPago(DateTime fechaDePago)
    {
        FechaDePago = fechaDePago;
    }
}