using AutoMapper;
using Domain.Model.Entities;
using Domain.Model.Entities.Usuario;
using DrivenAdapters.Mongo.Entities.Usuarios;

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
        }
    }
}