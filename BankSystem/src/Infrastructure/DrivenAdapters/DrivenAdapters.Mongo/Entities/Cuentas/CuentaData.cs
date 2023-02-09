using Domain.Model.Entities.Cuenta;
using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.Entities.Cuentas;

/// <summary>
/// Cuenta DTO
/// </summary>
public class CuentaData : EntityBase, IDomainEntity<Cuenta>
{
    /// <summary>
    /// Usuario Id
    /// </summary>
    [BsonElement("usuarioId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UsuarioId { get; set; }

    /// <summary>
    /// Banco
    /// </summary>
    public Banco Banco { get; set; }

    /// <summary>
    /// Capacidad de Endeudamiento
    /// </summary>
    [BsonElement("capacidad_endeudamiento")]
    public int CapacidadEndeudamiento { get; set; }

    /// <summary>
    /// Crea una instancia con todos los atributos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usuarioId"></param>
    /// <param name="banco"></param>
    /// <param name="capacidadEndeudamiento"></param>
    public CuentaData(string id, string usuarioId, Banco banco, int capacidadEndeudamiento)
    {
        Id = id;
        UsuarioId = usuarioId;
        Banco = banco;
        CapacidadEndeudamiento = capacidadEndeudamiento;
    }

    /// <summary>
    /// Crea una instancia sin el Id
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <param name="banco"></param>
    /// <param name="capacidadEndeudamiento"></param>
    public CuentaData(string usuarioId, Banco banco, int capacidadEndeudamiento)
    {
        UsuarioId = usuarioId;
        Banco = banco;
        CapacidadEndeudamiento = capacidadEndeudamiento;
    }

    /// <summary>
    /// Mapper a entidad de dominio
    /// </summary>
    /// <returns></returns>
    public Cuenta AsEntity() => new(Id, UsuarioId, Banco, CapacidadEndeudamiento);
}