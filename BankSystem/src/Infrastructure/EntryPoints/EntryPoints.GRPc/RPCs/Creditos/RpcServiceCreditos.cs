using AutoMapper;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Usuario;
using Domain.UseCase.Creditos;
using Domain.UseCase.Usuarios;
using EntryPoints.GRPc.dtos;
using EntryPoints.GRPc.RPCs.Base;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace EntryPoints.GRPc.RPCs.Creditos;

using GrpcModels = Protos;

public class RpcServiceCreditos : GrpcModels.UsuariosServices.UsuariosServicesBase
{
    private readonly IUsuarioUseCase _usuarioUseCase;
    private readonly ICreditoUseCase _creditoUseCase;
    private readonly IMapper _mapper;

    public RpcServiceCreditos(IUsuarioUseCase usuarioUseCase, ICreditoUseCase creditoUseCase, IMapper mapper)
    {
        _usuarioUseCase = usuarioUseCase;
        _creditoUseCase = creditoUseCase;
        _mapper = mapper;
    }

    public override Task<GrpcModels.StandardResponse> CrearCredito(GrpcModels.CrearCreditoRequest request,
        ServerCallContext context) => RpcServiceHandler.ProcessResponse(
        async () =>
        {
            var creditoDto = _mapper.Map<CrearCredito>(request);
            var creditoARetornar = await _creditoUseCase.Crear(_mapper.Map<Credito>(creditoDto));
            return _mapper.Map<GrpcModels.Credito>(creditoARetornar);
        });

    public override Task<GrpcModels.StandardResponse> CrearUsuario(GrpcModels.CrearUsuarioRequest request,
        ServerCallContext context) => RpcServiceHandler.ProcessResponse(
        async () =>
        {
            var usuarioDto = _mapper.Map<CrearUsuario>(request);
            var usuarioARetornar = await _usuarioUseCase.Crear(_mapper.Map<Usuario>(usuarioDto));
            return _mapper.Map<GrpcModels.Usuario>(usuarioARetornar);
        });
}