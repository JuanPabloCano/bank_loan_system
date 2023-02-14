using Domain.Model.Entities.Pago;
using DrivenAdapters.Mongo.Entities.Creditos;
using DrivenAdapters.Mongo.Entities.Cuentas;
using DrivenAdapters.Mongo.Entities.Pagos;
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
        /// Coleccion de Tipo <see cref="Usuarios"/>
        /// </summary>
        public IMongoCollection<UsuarioData> Usuarios { get; }

        /// <summary>
        /// Coleccion de tipo <see cref="Cuentas"/>
        /// </summary>
        public IMongoCollection<CuentaData> Cuentas { get; }

        /// <summary>
        /// Coleccion de tipo <see cref="Creditos"/>
        /// </summary>
        public IMongoCollection<CreditoData> Creditos { get; }

        /// <summary>
        /// Coleccion de tipo <see cref="Pago"/>
        /// </summary>
        public IMongoCollection<PagoData> Pagos { get; }
    }
}