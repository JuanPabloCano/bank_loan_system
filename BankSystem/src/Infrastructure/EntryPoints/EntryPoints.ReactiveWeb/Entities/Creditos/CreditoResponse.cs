using Domain.Model.Entities.Credito;

namespace EntryPoints.ReactiveWeb.Entities.Creditos;

/// <summary>
/// Clase de respuesta de entidad <see cref="Credito"/>
/// </summary>
public class CreditoResponse
{
    /// <summary>
    /// Metodo que retorna una entidad de tipo <see cref="Credito"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="credito"></param>
    /// <returns></returns>
    public static object Exec(string id, Credito credito) => new
    {
        Id = id,
        credito.UsuarioId,
        credito.CuentaId,
        credito.TotalPrestamo,
        credito.DeudaActual
    };
}