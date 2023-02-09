using Domain.Model.Entities.Gateway.Base;

namespace Domain.Model.Entities.Gateway;

/// <summary>
/// Interface de repositorio de entidad Usuario
/// </summary>
public interface IUsuarioRepository: IBaseRepository<Usuario.Usuario, string>
{
    
}