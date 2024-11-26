using Grpc.Net.Client;
using GrpcService;
using Microsoft.Extensions.Configuration;

namespace grpc_test
{
    public class GreatGrpcClientService: IGreatGrpcClientService
    {
        private readonly IConfiguration _configuration;

        public GreatGrpcClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SayHelloAsync(string name)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcAddress"), new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Greeter.GreeterClient(channel);

            var _ret = await client.SayHelloAsync(new() { Name=name});
            return _ret.Message;
        }
    }
}
