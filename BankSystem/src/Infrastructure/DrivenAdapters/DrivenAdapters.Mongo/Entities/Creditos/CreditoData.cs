using Domain.Model.Entities.Credito;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DrivenAdapters.Mongo.Entities.Creditos;

/// <summary>
/// Credito DTO
/// </summary>
[BsonIgnoreExtraElements]
public class CreditoData : EntityBase, IDomainEntity<Credito>
{
    /// <summary>
    /// UsuarioId
    /// </summary>
    [JsonProperty("usuarioId")]
    [BsonElement("usuarioId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UsuarioId { get; set; }

    /// <summary>
    /// CuentaId
    /// </summary>
    [JsonProperty("cuentaId")]
    [BsonElement("cuentaId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CuentaId { get; set; }

    /// <summary>
    /// Total Prestamo
    /// </summary>
    [BsonElement("totalPrestamo")]
    public int TotalPrestamo { get; set; }

    /// <summary>
    /// Deuda Actual
    /// </summary>
    [BsonElement("deudaActual")]
    public int DeudaActual { get; set; }

    /// <summary>
    /// Constructor vacio
    /// </summary>
    public CreditoData()
    {
    }

    /// <summary>
    /// Crea una instancia sin el Id de la clase <see cref="CreditoData"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioId"></param>
    /// <param name="cuentaId"></param>
    /// <param name="totalPrestamo"></param>
    /// <param name="deudaActual"></param>
    public CreditoData(string id, string usuarioId, string cuentaId, int totalPrestamo, int deudaActual)
    {
        Id = id;
        UsuarioId = usuarioId;
        CuentaId = cuentaId;
        TotalPrestamo = totalPrestamo;
        DeudaActual = deudaActual;
    }

    /// <summary>
    /// Crea una instancia con todos los atributos de la clase <see cref="CreditoData"/>
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <param name="cuentaId"></param>
    /// <param name="totalPrestamo"></param>
    /// <param name="deudaActual"></param>
    public CreditoData(string usuarioId, string cuentaId, int totalPrestamo, int deudaActual)
    {
        UsuarioId = usuarioId;
        CuentaId = cuentaId;
        TotalPrestamo = totalPrestamo;
        DeudaActual = deudaActual;
    }

    /// <summary>
    /// Mapper a entidad de dominio
    /// </summary>
    /// <returns></returns>
    public Credito AsEntity() => new(Id, UsuarioId, CuentaId, TotalPrestamo, DeudaActual);
}