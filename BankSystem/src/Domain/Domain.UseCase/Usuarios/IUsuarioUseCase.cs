using Domain.Model.Entities.Usuario;
using Domain.UseCase.Base;

namespace Domain.UseCase.Usuarios;

/// <summary>
/// Interface de entidad Usuario
/// </summary>
public interface IUsuarioUseCase : IBaseUseCase<Usuario, string>
{
}