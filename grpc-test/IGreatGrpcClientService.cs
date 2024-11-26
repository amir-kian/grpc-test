namespace grpc_test
{
    public interface IGreatGrpcClientService
    {
        Task<string> SayHelloAsync(string name);
    }
}
