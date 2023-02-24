namespace Domain.Model.Entities.Credito;

/// <summary>
/// Clase Credito
/// </summary>
public class Credito
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
    /// Constructor vacio
    /// </summary>
    public Credito()
    {
    }

    /// <summary>
    /// Crea una instancia de la clase Credito con todos los atributos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioId"></param>
    /// <param name="cuentaId"></param>
    /// <param name="totalPrestamo"></param>
    /// <param name="deudaActual"></param>
    public Credito(string id, string usuarioId, string cuentaId, int totalPrestamo, int deudaActual)
    {
        Id = id;
        UsuarioId = usuarioId;
        CuentaId = cuentaId;
        TotalPrestamo = totalPrestamo;
        DeudaActual = deudaActual;
    }

    /// <summary>
    /// Metodo para realizar un pago
    /// </summary>
    /// <param name="cantidad"></param>
    /// <returns></returns>
    public void RealizarPago(int cantidad) => DeudaActual -= cantidad;
}