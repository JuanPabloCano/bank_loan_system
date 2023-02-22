using AutoMapper;
using Domain.Model.Entities;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Usuario;
using DrivenAdapters.Mongo.Entities.Usuarios;
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
            CreateMap<Usuario, UsuarioData>().ReverseMap();
            CreateMap<Usuario, GrpcModels.Usuario>().ReverseMap();
            CreateMap<Credito, GrpcModels.Credito>().ReverseMap();
        }
    }
}