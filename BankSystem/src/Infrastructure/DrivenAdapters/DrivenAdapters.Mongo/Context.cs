using System.Diagnostics.CodeAnalysis;
using DrivenAdapters.Mongo.Entities.Cuentas;
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
        /// Tipo Contrato Usuarios
        /// </summary>
        public IMongoCollection<UsuarioData> Usuarios => _database.GetCollection<UsuarioData>("Usuarios");

        /// <summary>
        /// Tipo Contrato Cuentas
        /// </summary>
        public IMongoCollection<CuentaData> Cuentas => _database.GetCollection<CuentaData>("Cuentas");
    }
}