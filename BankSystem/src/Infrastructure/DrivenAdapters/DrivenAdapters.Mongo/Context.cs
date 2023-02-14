using System.Diagnostics.CodeAnalysis;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Entities.Pago;
using Domain.Model.Entities.Usuario;
using DrivenAdapters.Mongo.Entities.Creditos;
using DrivenAdapters.Mongo.Entities.Cuentas;
using DrivenAdapters.Mongo.Entities.Pagos;
using DrivenAdapters.Mongo.Entities.Usuarios;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Context is an implementation of <see cref="IContext"/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Context : IContext
    {
        private readonly IMongoDatabase _database;

        /// <summary>
        /// crea una nueva instancia de la clase <see cref="Context"/>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        public Context(string connectionString, string databaseName)
        {
            MongoClient mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(databaseName);
        }

        /// <summary>
        /// Tipo Contrato <see cref="Usuario"/>
        /// </summary>
        public IMongoCollection<UsuarioData> Usuarios => _database.GetCollection<UsuarioData>("Usuarios");

        /// <summary>
        /// Tipo Contrato <see cref="Cuenta"/>
        /// </summary>
        public IMongoCollection<CuentaData> Cuentas => _database.GetCollection<CuentaData>("Cuentas");

        /// <summary>
        /// Tipo contrato <see cref="Credito"/>
        /// </summary>
        public IMongoCollection<CreditoData> Creditos => _database.GetCollection<CreditoData>("Creditos");

        /// <summary>
        /// Tipo de contrato <see cref="Pago"/>
        /// </summary>
        public IMongoCollection<PagoData> Pagos => _database.GetCollection<PagoData>("Pagos");
    }
}