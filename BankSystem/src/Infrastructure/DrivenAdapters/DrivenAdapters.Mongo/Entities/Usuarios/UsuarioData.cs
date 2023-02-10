using Domain.Model.Entities.Usuario;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.Entities.Usuarios;

/// <summary>
/// Usuario DTO 
/// </summary>
[BsonIgnoreExtraElements]
public class UsuarioData : EntityBase, IDomainEntity<Usuario>
{
    /// <summary>
    /// Nombre
    /// </summary>
    [BsonElement("nombre")]
    public string Nombre { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    [BsonElement("apellido")]
    public string Apellido { get; set; }

    /// <summary>
    /// Cedula
    /// </summary>
    [BsonElement("cedula")]
    public string Cedula { get; set; }

    /// <summary>
    /// Correo
    /// </summary>
    [BsonElement("correo")]
    public string Correo { get; set; }

    /// <summary>
    /// Edad
    /// </summary>
    [BsonElement("edad")]
    public int Edad { get; set; }

    /// <summary>
    /// Profesion
    /// </summary>
    [BsonElement("profesion")]
    public string Profesion { get; set; }

    /// <summary>
    /// Constructor vacio
    /// </summary>
    public UsuarioData()
    {
    }

    /// <summary>
    /// Crea una instancia con todos los atributos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="cedula"></param>
    /// <param name="correo"></param>
    /// <param name="edad"></param>
    /// <param name="profesion"></param>
    public UsuarioData(string id, string nombre, string apellido, string cedula, string correo, int edad,
        string profesion)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Cedula = cedula;
        Correo = correo;
        Edad = edad;
        Profesion = profesion;
    }

    /// <summary>
    /// Crea una instancia sin el Id
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="cedula"></param>
    /// <param name="correo"></param>
    /// <param name="edad"></param>
    /// <param name="profesion"></param>
    public UsuarioData(string nombre, string apellido, string cedula, string correo, int edad, string profesion)
    {
        Nombre = nombre;
        Apellido = apellido;
        Cedula = cedula;
        Correo = correo;
        Edad = edad;
        Profesion = profesion;
    }

    /// <summary>
    /// Mapper a entidad de dominio
    /// </summary>
    /// <returns></returns>
    public Usuario AsEntity() => new(Id, Nombre, Apellido, Cedula, Correo, Edad, Profesion);
}