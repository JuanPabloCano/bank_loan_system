using AutoMapper;
using Domain.Model.Entities;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Usuario;
using DrivenAdapters.Mongo.Entities.Usuarios;
using EntryPoints.GRPc.dtos;
using GrpcModels = EntryPoints.GRPc.Protos;

namespace BankSystem.AppServices.Automapper
{
    /// <summary>
    /// EntityProfile
    /// </summary>
    public class ConfigurationProfile : Profile
    {
        /// <summary>
        /// ConfigurationProfile
        /// </summary>
        public ConfigurationProfile()
        {
            CreateMap<Usuario, UsuarioData>();

            CreateMap<CrearUsuario, Usuario>();
            CreateMap<Usuario, CrearUsuario>();

            CreateMap<CrearCredito, Credito>();
            CreateMap<Credito, CrearCredito>();

            CreateMap<GrpcModels.CrearUsuarioRequest, CrearUsuario>();
            CreateMap<GrpcModels.CrearCreditoRequest, CrearCredito>();

            CreateMap<Usuario, GrpcModels.Usuario>();
            CreateMap<Credito, GrpcModels.Credito>();
        }
    }
}