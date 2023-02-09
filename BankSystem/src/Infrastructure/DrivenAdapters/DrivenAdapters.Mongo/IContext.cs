using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Entities.Cuentas;
using DrivenAdapters.Mongo.Entities.Usuarios;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Interfaz Mongo context contract.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Coleccion de Tipo Usuarios
        /// </summary>
        public IMongoCollection<UsuarioData> Usuarios { get; }

        /// <summary>
        /// Coleccion de tipo Cuenta
        /// </summary>
        public IMongoCollection<CuentaData> Cuentas { get; }
    }
}