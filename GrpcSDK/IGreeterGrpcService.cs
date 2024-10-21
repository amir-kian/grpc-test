using Grpc.Core;
using GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcSDK;

public interface IGreeterGrpcService
{
	Task<string> SayHelloAsync(string name, CancellationToken cancellationToken);
}

public class GreeterGrpcService : IGreeterGrpcService
{
	private readonly Greeter.GreeterClient _grpcClient;
	public GreeterGrpcService(Greeter.GreeterClient greeterClient)
	{
		_grpcClient = greeterClient;

	}
	public async Task<string> SayHelloAsync(string name, CancellationToken cancellationToken)
	{
		try
		{
			var response = await _grpcClient.SayHelloAsync(new HelloRequest { Name = name });
			return response.Message; // Assuming HelloReply has a property called Message
		}
		catch (RpcException ex)
		{
			throw;
		}

	}
}
