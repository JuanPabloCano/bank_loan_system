using System.Text.Json.Serialization;
using Domain.Model.ValueObjects;

namespace Domain.Model.Entities.Cuenta;

/// <summary>
/// Clase Cuenta
/// </summary>
public class Cuenta
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; }

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
    public int CapacidadEndeudamiento { get; private set; }

    /// <summary>
    /// Propiedad virtual de la entidad <see cref="Usuario"/>
    /// </summary>
    public virtual Usuario.Usuario Usuario { get; set; }

    /// <summary>
    /// Constructor vacio
    /// </summary>
    public Cuenta()
    {
    }

    /// <summary>
    /// Crea una instancia de la clase Cuenta con todos los atributos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioId"></param>
    /// <param name="banco"></param>
    /// <param name="capacidadEndeudamiento"></param>
    public Cuenta(string id, string usuarioId, Banco banco, int capacidadEndeudamiento)
    {
        Id = id;
        UsuarioId = usuarioId;
        Banco = banco;
        CapacidadEndeudamiento = capacidadEndeudamiento;
    }

    /// <summary>
    /// Metodo para agregar capacidad de endeudamiento
    /// </summary>
    /// <param name="total"></param>
    public void AñadirCapacidadEndeudamiento(int total) => CapacidadEndeudamiento = total;
}