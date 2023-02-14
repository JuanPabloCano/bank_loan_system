using Domain.Model.Entities.Pago;
using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DrivenAdapters.Mongo.Entities.Pagos;

/// <summary>
/// Pago DTO
/// </summary>
[BsonIgnoreExtraElements]
public class PagoData : EntityBase, IDomainEntity<Pago>
{
    /// <summary>
    /// Usuario Id
    /// </summary>
    [JsonProperty("usuarioId")]
    [BsonElement("usuarioId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UsuarioId { get; set; }

    /// <summary>
    /// Credito Id
    /// </summary>
    [JsonProperty("creditoId")]
    [BsonElement("creditoId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CreditoId { get; set; }

    /// <summary>
    /// Cantidad
    /// </summary>
    [BsonElement("cantidad")]
    public int Cantidad { get; set; }

    /// <summary>
    /// Fecha de Pago
    /// </summary>
    public FechaPago FechaPago { get; set; }

    /// <summary>
    /// Constructor vacio
    /// </summary>
    public PagoData()
    {
    }

    /// <summary>
    /// Crea una instancia con todos los atributos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioId"></param>
    /// <param name="creditoId"></param>
    /// <param name="cantidad"></param>
    /// <param name="fechaPago"></param>
    public PagoData(string id, string usuarioId, string creditoId, int cantidad, FechaPago fechaPago)
    {
        Id = id;
        UsuarioId = usuarioId;
        CreditoId = creditoId;
        Cantidad = cantidad;
        FechaPago = fechaPago;
    }

    /// <summary>
    /// Crea una instancia sin el Id
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <param name="creditoId"></param>
    /// <param name="cantidad"></param>
    /// <param name="fechaPago"></param>
    public PagoData(string usuarioId, string creditoId, int cantidad, FechaPago fechaPago)
    {
        UsuarioId = usuarioId;
        CreditoId = creditoId;
        Cantidad = cantidad;
        FechaPago = fechaPago;
    }

    /// <summary>
    /// Mapper a entidad de dominio
    /// </summary>
    /// <returns></returns>
    public Pago AsEntity() => new(Id, UsuarioId, CreditoId, Cantidad, FechaPago);
}