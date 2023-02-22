using credinet.exception.middleware.models;
using Google.Protobuf.WellKnownTypes;
using EntryPoints.GRPc.Protos;

namespace EntryPoints.GRPc.RPCs.Base;

public class RpcServiceHandler
{
    public static async Task<StandardResponse> ProcessResponse<TResponse>(Func<Task<TResponse>> responseProvider) where TResponse : class
        {
            StandardResponse response;
            try
            {
                TResponse result = await responseProvider();
                Any any = Any.Pack((dynamic)result);
                response = new()
                {
                    Data = any,
                    Error = false,
                    Message = ""
                };
            }
            catch (BusinessException exception)
            {
                response = new()
                {
                    Data = new() { },
                    Error = true,
                    Message = $"Excepción de negocio: {exception.Message}"
                };
            }
            catch (Exception exception)
            {
                response = new()
                {
                    Data = new() { },
                    Error = true,
                    Message = $"Excepción desconocida: {exception.Message}"
                };
            }
    
            return response;
        }
}