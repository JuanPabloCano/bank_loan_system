using Domain.Model.Entities.Cuenta;

namespace EntryPoints.ReactiveWeb.Entities.Cuentas;

/// <summary>
/// Clase de respuesta de entidad <see cref="Cuenta"/>
/// </summary>
public class CuentaResponse
{
    /// <summary>
    /// Metodo que retorna una entidad de tipo <see cref="Cuenta"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cuenta"></param>
    /// <returns></returns>
    public static object Exec(string id, Cuenta cuenta) => new
    {
        Id = id,
        cuenta.UsuarioId,
        cuenta.Banco,
        cuenta.CapacidadEndeudamiento
    };
}