using Grpc.Core.Interceptors;
using Grpc.Core;

namespace SportingEvents.API.Interceptors
{
    public class ExceptionInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                var httpContext = context.GetHttpContext();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
